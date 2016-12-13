#multiAgents.py (original)

# multiAgents.py
# --------------
# Licensing Information: Please do not distribute or publish solutions to this
# project. You are free to use and extend these projects for educational
# purposes. The Pacman AI projects were developed at UC Berkeley, primarily by
# John DeNero (denero@cs.berkeley.edu) and Dan Klein (klein@cs.berkeley.edu).
# For more info, see http://inst.eecs.berkeley.edu/~cs188/sp09/pacman.html

from util import manhattanDistance
from game import Directions
import random, util
import math

from game import Agent

INFINITY = 99999999999999

class ReflexAgent(Agent):
  """
    A reflex agent chooses an action at each choice point by examining
    its alternatives via a state evaluation function.

    The code below is provided as a guide.  You are welcome to change
    it in any way you see fit, so long as you don't touch our method
    headers.
  """
  
  def getAction(self, gameState):
    """
    You do not need to change this method, but you're welcome to.

    getAction chooses among the best options according to the evaluation function.

    Just like in the previous project, getAction takes a GameState and returns
    some Directions.X for some X in the set {North, South, West, East, Stop}
    """
    # Collect legal moves and successor states
    legalMoves = gameState.getLegalActions()

    # Choose one of the best actions
    scores = [self.evaluationFunction(gameState, action) for action in legalMoves]

    bestScore = max(scores)
    bestIndices = [index for index in range(len(scores)) if scores[index] == bestScore]
    chosenIndex = random.choice(bestIndices) # Pick randomly among the best

    "Add more of your code here if you want to"

    return legalMoves[chosenIndex]
  
  def evaluationFunction(self, currentGameState, action):
    """
    Design a better evaluation function here.

    The evaluation function takes in the current and proposed successor
    GameStates (pacman.py) and returns a number, where higher numbers are better.

    The code below extracts some useful information from the state, like the
    remaining food (oldFood) and Pacman position after moving (newPos).
    newScaredTimes holds the number of moves that each ghost will remain
    scared because of Pacman having eaten a power pellet.

    Print out these variables to see what you're getting, then combine them
    to create a masterful evaluation function.
    """

    # Useful information you can extract from a GameState (pacman.py)
    successorGameState = currentGameState.generatePacmanSuccessor(action)
    newPos = successorGameState.getPacmanPosition()
    oldFood = currentGameState.getFood()
    newGhostStates = successorGameState.getGhostStates()
    newScaredTimes = [ghostState.scaredTimer for ghostState in newGhostStates]

    "*** YOUR CODE HERE ***"

    curPos = currentGameState.getPacmanPosition()
    GhostPositions = [ghostState.getPosition() for ghostState in newGhostStates]
    FoodPositions = oldFood.asList()
    Walls = currentGameState.getWalls() 

    GhostScore = 0
    FoodScore = 0

    smallGhostMD = [Pos for Pos in GhostPositions if manhattanDistance(curPos,Pos) <= 2]
    if 0 < len(smallGhostMD):

      if 1 < len(smallGhostMD):
        print "two close ghosts!"
        return 0
      smallGhostMD = smallGhostMD[0]

      if manhattanDistance(curPos,smallGhostMD) < manhattanDistance(newPos,smallGhostMD): 
        GhostScore += 10

    else: 

      for ghostState in newGhostStates:

        GhostPos = util.nearestPoint(ghostState.getPosition())
        GhostDirection = ghostState.getDirection()

        if GhostPos[0] == curPos[0]:
          NoWallBetweenAs = not (True in [ Walls[GhostPos[0]][index] for index in range( min(GhostPos[1],curPos[1]) + 1, max(GhostPos[1],curPos[1]) ) ])
          HeadingMyDirection = (curPos[1] < GhostPos[1] and GhostDirection == 'South') or (curPos[1] > GhostPos[1] and GhostDirection == 'North')
          if NoWallBetweenAs and HeadingMyDirection:
            if action == 'East' or action == 'West': 
              GhostScore += 10
            elif abs(GhostPos[1] - curPos[1]) < abs(GhostPos[1] - newPos[1]):
              GhostScore += 10

        if GhostPos[1] == curPos[1]:
          NoWallBetweenAs = not (True in [ Walls[index][GhostPos[1]] for index in range( min(GhostPos[0],curPos[0]) + 1, max(GhostPos[0],curPos[0]) ) ])
          HeadingMyDirection = (curPos[0] < GhostPos[0] and GhostDirection == 'West') or (curPos[0] > GhostPos[0] and GhostDirection == 'East')
          if NoWallBetweenAs and HeadingMyDirection:
            if action == 'North' or action == 'South': 
              GhostScore += 10
            elif abs(GhostPos[0] - curPos[0]) < abs(GhostPos[0] - newPos[0]):
              GhostScore += 10


    curFoodMD = [manhattanDistance(curPos,Pos) for Pos in FoodPositions]
    minCurFoodMD = min(curFoodMD)
    minIndex = curFoodMD.index(minCurFoodMD)
    minFood = FoodPositions[minIndex]
    
    if manhattanDistance(newPos,minFood) < manhattanDistance(curPos,minFood):
      WallBetweenAs = False
      if minFood[1] == curPos[1]:
        WallBetweenAs = True in [ Walls[index][minFood[1]] for index in range( min(minFood[0],curPos[0]) + 1, max(minFood[0],curPos[0]) ) ]
      if minFood[0] == curPos[0]:
        WallBetweenAs = True in [ Walls[minFood[0]][index] for index in range( min(minFood[1],curPos[1]) + 1, max(minFood[1],curPos[1]) ) ]
      if not WallBetweenAs: 
        FoodScore += 2

    score = GhostScore + FoodScore
    return score

  
def scoreEvaluationFunction(currentGameState):
  """
    This default evaluation function just returns the score of the state.
    The score is the same one displayed in the Pacman GUI.

    This evaluation function is meant for use with adversarial search agents
    (not reflex agents).
  """
  return currentGameState.getScore()

class MultiAgentSearchAgent(Agent):
  """
    This class provides some common elements to all of your
    multi-agent searchers.  Any methods defined here will be available
    to the MinimaxPacmanAgent, AlphaBetaPacmanAgent & ExpectimaxPacmanAgent.

    You *do not* need to make any changes here, but you can if you want to
    add functionality to all your adversarial search agents.  Please do not
    remove anything, however.

    Note: this is an abstract class: one that should not be instantiated.  It's
    only partially specified, and designed to be extended.  Agent (game.py)
    is another abstract class.
  """

  def __init__(self, evalFn = 'scoreEvaluationFunction', depth = '2'):
    self.index = 0 # Pacman is always agent index 0
    self.evaluationFunction = util.lookup(evalFn, globals())
    self.depth = int(depth)

class MinimaxAgent(MultiAgentSearchAgent):
  """
    Your minimax agent (question 2)
  """

  def getAction(self, gameState):
    """
      Returns the minimax action from the current gameState using self.depth
      and self.evaluationFunction.

      Here are some method calls that might be useful when implementing minimax.

      gameState.getLegalActions(agentIndex):
        Returns a list of legal actions for an agent
        agentIndex=0 means Pacman, ghosts are >= 1

      Directions.STOP:
        The stop direction, which is always legal

      gameState.generateSuccessor(agentIndex, action):
        Returns the successor game state after an agent takes an action

      gameState.getNumAgents():
        Returns the total number of agents in the game
    """
    "*** YOUR CODE HERE ***"
    
    action, maxval = self.MinmaxSearch(gameState, 0, self.depth)
    return action


  def MinmaxSearch(self, gameState, agentIndex, depth):
    
    if depth == 0 or gameState.isLose() or gameState.isWin():
      return self.evaluationFunction(gameState)

    legalActions = gameState.getLegalActions(agentIndex)
    #if 'Stop' in legalActions:
    #  legalActions.remove('Stop')
    
    if agentIndex == 0: 

      # ---------- Pacman turn 
      bestAction = ''
      curMax = -INFINITY
      for action in legalActions:
        successorGameState = gameState.generateSuccessor(0, action)
        cur = self.MinmaxSearch(successorGameState, 1, depth)
        if curMax < cur:
          curMax = cur
          bestAction = action 

      if depth == self.depth:
        return bestAction, curMax
      else: 
        return curMax

    else:

      # ---------- Ghost turn 
      curMin = INFINITY
      for action in legalActions:
        successorGameState = gameState.generateSuccessor(agentIndex, action)
        if agentIndex + 1 < gameState.getNumAgents():
          cur = self.MinmaxSearch(successorGameState, agentIndex + 1, depth)
        else:
          cur = self.MinmaxSearch(successorGameState, 0, depth - 1)
        curMin = min(curMin, cur)

      return curMin


class AlphaBetaAgent(MultiAgentSearchAgent):
  """
    Your minimax agent with alpha-beta pruning (question 3)
  """

  def getAction(self, gameState):
    """
      Returns the minimax action using self.depth and self.evaluationFunction
    """
    "*** YOUR CODE HERE ***"
        
    action, maxval = self.AlphaBetaSearch(gameState, 0, self.depth, -INFINITY, INFINITY)
    return action


  def AlphaBetaSearch(self, gameState, agentIndex, depth, alpha, beta):
    
    if depth == 0 or gameState.isLose() or gameState.isWin():
      return self.evaluationFunction(gameState)

    legalActions = gameState.getLegalActions(agentIndex)
    #if 'Stop' in legalActions:
    #  legalActions.remove('Stop')
    
    if agentIndex == 0: 

      # ---------- Pacman turn 
      bestAction = ''
      curMax = -INFINITY
      for action in legalActions:

        successorGameState = gameState.generateSuccessor(0, action)
        cur = self.AlphaBetaSearch(successorGameState, 1, depth, alpha, beta)
        if curMax < cur:
          curMax = cur
          bestAction = action 
        alpha = max(alpha, curMax)
        if curMax >= beta: 
          return INFINITY

      if depth == self.depth:
        return bestAction, curMax
      else: 
        return curMax

    else:

      # ---------- Ghost turn 

      curMin = INFINITY
      for action in legalActions:

        successorGameState = gameState.generateSuccessor(agentIndex, action)
        if agentIndex + 1 < gameState.getNumAgents():
          cur = self.AlphaBetaSearch(successorGameState, agentIndex + 1, depth, alpha, beta)
        else:
          cur = self.AlphaBetaSearch(successorGameState, 0, depth - 1, alpha, beta)
        curMin = min(curMin, cur)
        beta = min(beta, curMin)
        if curMin <= alpha: 
          return -INFINITY

      return curMin


class ExpectimaxAgent(MultiAgentSearchAgent):
  """
    Your expectimax agent (question 4)
  """

  def getAction(self, gameState):
    """
      Returns the expectimax action using self.depth and self.evaluationFunction

      All ghosts should be modeled as choosing uniformly at random from their
      legal moves.
    """
    "*** YOUR CODE HERE ***"
    util.raiseNotDefined()

def betterEvaluationFunction(currentGameState):
  """
    Your extreme ghost-hunting, pellet-nabbing, food-gobbling, unstoppable
    evaluation function (question 5).

    DESCRIPTION: <write something here so we know what you did>
  """
  "*** YOUR CODE HERE ***"
  util.raiseNotDefined()

# Abbreviation
better = betterEvaluationFunction

class ContestAgent(MultiAgentSearchAgent):
  """
    Your agent for the mini-contest
  """

  def getAction(self, gameState):
    """
      Returns an action.  You can use any method you want and search to any depth you want.
      Just remember that the mini-contest is timed, so you have to trade off speed and computation.

      Ghosts don't behave randomly anymore, but they aren't perfect either -- they'll usually
      just make a beeline straight towards Pacman (or away from him if they're scared!)
    """
    "*** YOUR CODE HERE ***"
    util.raiseNotDefined()


  