from util import Pair
import copy
from propositionLayer import PropositionLayer
from planGraphLevel import PlanGraphLevel
from Parser import Parser
from action import Action

from proposition import Proposition

try:
  from search import SearchProblem
  from search import aStarSearch

except:
  from  CPF.search import SearchProblem 
  from  CPF.search import aStarSearch

class PlanningProblem():
  def __init__(self, domain, problem):
    """
    Constructor
    """
    p = Parser(domain, problem)
    self.actions, self.propositions = p.parseActionsAndPropositions()	
                                            # list of all the actions and list of all the propositions
    self.initialState, self.goal = p.pasreProblem() 				
                                            # the initial state and the goal state are lists of propositions
    self.createNoOps() 											# creates noOps that are used to propagate existing propositions from one layer to the next
    PlanGraphLevel.setActions(self.actions)
    PlanGraphLevel.setProps(self.propositions)
    self._expanded = 0

  def getStartState(self):
    "*** YOUR CODE HERE ***"

    # explain: A state is a planGraphLevel, 
    #   and here I am building a level with the initial state propositions

    propLayerInit = PropositionLayer()
    for prop in self.initialState:
      propLayerInit.addProposition(prop)
    pgInit = PlanGraphLevel()
    pgInit.setPropositionLayer(propLayerInit)

    return pgInit
    
  def isGoalState(self, state):
    """
    Hint: you might want to take a look at goalStateNotInPropLayer function
    """
    "*** YOUR CODE HERE ***"

    # explain: A state is a the goal state if it's propositions contains the goal propositions 
    #   without mutexs between them 

    gp = state
    propositions = gp.propositionLayer.propositions

    # check if propositions contain goal 
    isGoal = not self.goalStateNotInPropLayer(propositions)

    # check if propositions contain mutexs
    if isGoal:
      index = 0
      for prop1 in propositions:
        index += 1
        for prop2 in propositions[index:]:
          if Pair(prop1, prop2) in gp.propositionLayer.mutexPropositions:
            isGoal = False

    return isGoal
    
  def getSuccessors(self, state):
    """   
    For a given state, this should return a list of triples, 
    (successor, action, stepCost), where 'successor' is a 
    successor to the current state, 'action' is the action
    required to get there, and 'stepCost' is the incremental 
    cost of expanding to that successor, 1 in our case.
    You might want to this function:
    For a list of propositions l and action a,
    a.allPrecondsInList(l) returns true if the preconditions of a are in l
    """
    self._expanded += 1
    "*** YOUR CODE HERE ***"

    # explain: Successors are the list of actions can be done from the current level 
    #   I build the action lyer using pgNext.updateActionLayer
    #   then for each action I build a the Level that wholud have been created if only this action was selected

    Successors = []

    pg = state
    previousPropositionLayer = pg.getPropositionLayer()
    previousLayerPropositions = previousPropositionLayer.propositions

    pgNext = PlanGraphLevel() 
    pgNext.updateActionLayer(previousPropositionLayer)
    
    for Action in pgNext.actionLayer.actions:

      if Action.isNoOp():
        continue

      pgNextAction = PlanGraphLevel() 
      pgNextAction.actionLayer.addAction(Action)
      for prop in previousLayerPropositions:
        pgNextAction.propositionLayer.addProposition(prop)
      for prop in Action.getAdd():
        new_prop = Proposition(prop.getName())
        new_prop.addProducer(Action)
        pgNextAction.propositionLayer.addProposition(new_prop)
      for prop in Action.getDelete():
        pgNextAction.propositionLayer.removePropositions(prop)
      pgNextAction.updateMutexProposition()

      Successors.append( (pgNextAction, Action, 1))
      
    return Successors

  def getCostOfActions(self, actions):
    return len(actions)
    
  def goalStateNotInPropLayer(self, propositions):
    """
    Helper function that returns true if all the goal propositions 
    are in propositions
    """
    for goal in self.goal:
      if goal not in propositions:
        return True
    return False

  def createNoOps(self):
    """
    Creates the noOps that are used to propagate propositions from one layer to the next
    """
    for prop in self.propositions:
      name = prop.name
      precon = []
      add = []
      precon.append(prop)
      add.append(prop)
      delete = []
      act = Action(name,precon,add,delete, True)
      self.actions.append(act)  
      
def maxLevel(state, problem):
  """
  The heuristic value is the number of layers required to expand all goal propositions.
  If the goal is not reachable from the state your heuristic should return float('inf')  
  A good place to start would be:
  propLayerInit = PropositionLayer()          #create a new proposition layer
  for prop in state:
    propLayerInit.addProposition(prop)        #update the proposition layer with the propositions of the state
  pgInit = PlanGraphLevel()                   #create a new plan graph level (level is the action layer and the propositions layer)
  pgInit.setPropositionLayer(propLayerInit)   #update the new plan graph level with the the proposition layer
  """
  "*** YOUR CODE HERE ***"
  
    # explain: implement max level heuristic
    #   expand the graph with out mutexs until the goal is reached, 
    #   the heuristic value is the number of levels need to reach the goal 

  pg = state

  Graph = []
  Graph.append(pg)
  Level = 0
  
  pgNext = pg
  isRepeat = False
  isGoal = False
  while not isGoal and not isRepeat:

    # expand next level without mutexs 
    pgPrev = pgNext 
    pgNext = PlanGraphLevel() 
    pgNext.expandWithoutMutex(pgPrev)

    Graph.append(pgNext)
    Level += 1

    # check if level expansion is in 'levels-off' state 
    #  if isFixed() function return true' check if the current level was aread reached in a previous graph history 
    #  if so, we are in a loop state and the heuristic value should be 'inf' 

    if isFixed(Graph, Level):

      pgNextPropositions = pgNext.getPropositionLayer().getPropositions()
      isRepeat = True
      for Hist in range(Level-1):
        HistLevelPropositions = Graph[Hist].getPropositionLayer().getPropositions()
        for prop in pgNextPropositions:
          if prop not in HistLevelPropositions:
            isRepeat = False

    isGoal = not problem.goalStateNotInPropLayer(pgNext.propositionLayer.propositions)

  h = Level
  if isRepeat and not isGoal:
    h = float('inf')

  return h

def levelSum(state, problem):
  """
  The heuristic value is the sum of sub-goals level they first appeared.
  If the goal is not reachable from the state your heuristic should return float('inf')
  """
  "*** YOUR CODE HERE ***"
  
    # explain: implement max level heuristic
    #   expand the graph with out mutexs until the goal is reached, 
    #   the heuristic value is the sum of the levels reached for each goal proposition 

  pg = state

  Graph = []
  Graph.append(pg)
  Level = 0
  Sum = 0
  goal = copy.deepcopy(problem.goal);

  pgNext = pg
  isRepeat = False
  isGoal = False
  while not isGoal and not isRepeat:

    # expand next level without mutexs 
    pgPrev = pgNext 
    pgNext = PlanGraphLevel() 
    pgNext.expandWithoutMutex(pgPrev)

    Graph.append(pgNext)
    Level += 1

    # check if level expansion is in 'levels-off' state 
    #  if isFixed() function return true' check if the current level was aread reached in a previous graph history 
    #  if so, we are in a loop state and the heuristic value should be 'inf' 

    if isFixed(Graph, Level):
      pgNextPropositions = pgNext.getPropositionLayer().getPropositions()
      isRepeat = True
      for Hist in range(Level-1):
        HistLevelPropositions = Graph[Hist].getPropositionLayer().getPropositions()
        for prop in pgNextPropositions:
          if prop not in HistLevelPropositions:
            isRepeat = False

    to_delete = []
    for prop in goal:
      if prop in pgNext.propositionLayer.propositions:
        # add each goal level, and delete it from goal list 
        Sum += Level
        to_delete.append(prop)
    for prop in to_delete:
      goal.remove(prop)
    isGoal = len(goal) == 0

  h = Sum
  if isRepeat and not isGoal:
    h = float('inf')

  return h

def isFixed(Graph, level):
  """
  Checks if we have reached a fixed point,
  i.e. each level we'll expand would be the same, thus no point in continuing
  """
  if level == 0:
    return False  
  return len(Graph[level].getPropositionLayer().getPropositions()) == len(Graph[level - 1].getPropositionLayer().getPropositions())  
      
if __name__ == '__main__':
  import sys
  import time
  if len(sys.argv) != 1 and len(sys.argv) != 4:
    print("Usage: PlanningProblem.py domainName problemName heuristicName(max, sum or zero)")
    exit()
  domain = 'dwrDomain.txt'
  problem = 'dwrProblem.txt'
  heuristic = lambda x,y: 0
  if len(sys.argv) == 4:
    domain = str(sys.argv[1])
    problem = str(sys.argv[2])
    if str(sys.argv[3]) == 'max':
      heuristic = maxLevel
    elif str(sys.argv[3]) == 'sum':
      heuristic = levelSum
    elif str(sys.argv[3]) == 'zero':
      heuristic = lambda x,y: 0
    else:
      print("Usage: PlanningProblem.py domainName problemName heuristicName(max, sum or zero)")
      exit()

  prob = PlanningProblem(domain, problem)
  start = time.clock()
  plan = aStarSearch(prob, heuristic)  
  elapsed = time.clock() - start
  if plan is not None:
    print("Plan found with %d actions in %.2f seconds" % (len(plan), elapsed))
    print('plan ' + str([act.getName() for act in plan if not act.isNoOp()]))
  else:
    print("Could not find a plan in %.2f seconds" %  elapsed)
  print("Search nodes expanded: %d" % prob._expanded)
 
