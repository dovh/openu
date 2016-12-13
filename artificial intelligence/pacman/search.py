# search.py
# ---------
# Licensing Information: Please do not distribute or publish solutions to this
# project. You are free to use and extend these projects for educational
# purposes. The Pacman AI projects were developed at UC Berkeley, primarily by
# John DeNero (denero@cs.berkeley.edu) and Dan Klein (klein@cs.berkeley.edu).
# For more info, see http://inst.eecs.berkeley.edu/~cs188/sp09/pacman.html

"""
In search.py, you will implement generic search algorithms which are called 
by Pacman agents (in searchAgents.py).
"""

from game import Directions
import util

class SearchProblem:
  """
  This class outlines the structure of a search problem, but doesn't implement
  any of the methods (in object-oriented terminology: an abstract class).
  
  You do not need to change anything in this class, ever.
  """
  
  def getStartState(self):
     """
     Returns the start state for the search problem 
     """
     util.raiseNotDefined()
    
  def isGoalState(self, state):
     """
       state: Search state
    
     Returns True if and only if the state is a valid goal state
     """
     util.raiseNotDefined()

  def getSuccessors(self, state):
     """
       state: Search state
     
     For a given state, this should return a list of triples, 
     (successor, action, stepCost), where 'successor' is a 
     successor to the current state, 'action' is the action
     required to get there, and 'stepCost' is the incremental 
     cost of expanding to that successor
     """
     util.raiseNotDefined()

  def getCostOfActions(self, actions):
     """
      actions: A list of actions to take
 
     This method returns the total cost of a particular sequence of actions.  The sequence must
     be composed of legal moves
     """
     util.raiseNotDefined()
           

def tinyMazeSearch(problem):
  """
  Returns a sequence of moves that solves tinyMaze.  For any other
  maze, the sequence of moves will be incorrect, so only use this for tinyMaze
  """
  from game import Directions
  s = Directions.SOUTH
  w = Directions.WEST
  return  [s,s,w,s,w,w,s,w]


"***********************************************"
def depthFirstSearch_recursion(problem, state, frontier, explored):
  
  if frontier.isEmpty(): 
      util.raiseNotDefined() # failure 

  state = frontier.pop()
  explored.push(state)
  Successors = problem.getSuccessors(state)
  #Successors.reverse()

  for Succ in Successors:
    if Succ[0] not in frontier.list and Succ[0] not in explored.list:

      frontier.push(Succ[0]) 
      if problem.isGoalState(Succ[0]):
        return [Succ[1]]
      retval = depthFirstSearch_recursion(problem, Succ[0], frontier, explored)
      if retval != None:
        return [Succ[1]] + retval 

def depthFirstSearch(problem):
  """
  Search the deepest nodes in the search tree first [p 74].
  
  Your search algorithm needs to return a list of actions that reaches
  the goal.  Make sure to implement a graph search algorithm [Fig. 3.18].
  
  To get started, you might want to try some of these simple commands to
  understand the search problem that is being passed in:
  
  print "Start:", problem.getStartState()
  print "Is the start a goal?", problem.isGoalState(problem.getStartState())
  print "Start's successors:", problem.getSuccessors(problem.getStartState())
  """

  start = problem.getStartState()
  frontier = util.Stack()
  explored = util.Stack()
  frontier.push(start)

  path = depthFirstSearch_recursion(problem, start, frontier, explored)
  return path
    
"***********************************************"
class Node:

  def __init__(self, state, parent, action, g, h):
    self.state = state
    self.parent = parent
    self.action = action
    self.g = g; 
    self.h = h; 
    self.f = g + h

  def solution(self, problem):
    curr = self
    retval = []
    while not curr.parent == None:
      retval.insert(0,curr.action)
      curr = curr.parent
    return retval
      
def find_state1(nodes, state):
  for node in nodes:
    if node.state == state: 
      return True
  return False
    
def breadthFirstSearch(problem):
  "Search the shallowest nodes in the search tree first. [p 74]"

  root = problem.getStartState()
  if problem.isGoalState(root):
    return [root]

  frontier = util.Queue()
  explored = util.Queue()
  frontier.push( Node(root, None, Directions.STOP, 0, 0) )

  while True:

    if frontier.isEmpty(): 
      util.raiseNotDefined() # return failure 

    node = frontier.pop()
    explored.push(node.state)

    for child in problem.getSuccessors(node.state):
      if not find_state1(frontier.list, child[0]) and child[0] not in explored.list:
        child_node = Node(child[0], node, child[1], 0, 0)
        if problem.isGoalState(child[0]): 
          return child_node.solution(problem)
        frontier.push(child_node)
        
  util.raiseNotDefined()
      
"***********************************************"

def find_state2(nodes, state):
  for node in nodes:
    if node[1].state == state: 
      return node
  return None

def uniformCostSearch(problem):
  "Search the node of least total cost first. "

  root = problem.getStartState()
  if problem.isGoalState(root):
    return [root]

  frontier = util.PriorityQueue()
  explored = util.Queue()
  frontier.push( Node(root, None, Directions.STOP, 0, 0), 0 )

  while True:

    if frontier.isEmpty(): 
      util.raiseNotDefined() # return failure 

    node = frontier.pop()
    if problem.isGoalState(node.state): 
      return node.solution(problem)

    explored.push(node.state)

    for child in problem.getSuccessors(node.state):
      child_node = Node(child[0], node, child[1], node.g + child[2], 0)
      frontier_node = find_state2(frontier.heap, child[0])
      if frontier_node == None and child[0] not in explored.list:
        frontier.push(child_node, child_node.g)
      elif frontier_node != None and child_node.g < frontier_node[1].g:
        frontier.heap.remove(frontier_node)
        frontier.push(child_node, child_node.g)

  util.raiseNotDefined()

"***********************************************"

def nullHeuristic(state, problem=None):
  """
  A heuristic function estimates the cost from the current state to the nearest
  goal in the provided SearchProblem.  This heuristic is trivial.
  """
  return 0

def find_state3(nodes, state):
  for node in nodes:
    if node.state == state: 
      return node
  return None

def aStarSearch(problem, heuristic=nullHeuristic):
  "Search the node that has the lowest combined cost and heuristic first."
  
  root = problem.getStartState()
  h = heuristic(root, problem)

  frontier = util.PriorityQueue()
  explored = util.Queue()
  frontier.push( Node(root, None, Directions.STOP, 0, h), h )

  while not frontier.isEmpty():

    node = frontier.pop()
    explored.push(node)
    if problem.isGoalState(node.state):
      return node.solution(problem)

    for child in problem.getSuccessors(node.state):
      
      child_node = Node(child[0], node, child[1], node.g + child[2], heuristic(child[0], problem))

      frontier_node = find_state2(frontier.heap, child[0]) 
      explored_node = find_state3(explored.list, child[0])

      if frontier_node != None:
        if child_node.g < frontier_node[1].g:
          frontier.heap.remove(frontier_node)
          frontier.push(child_node, child_node.f)
      elif explored_node != None:
        if child_node.g < explored_node.g:
          explored.list.remove(explored_node)
          frontier.push(child_node, child_node.f)
      else:
        frontier.push(child_node, child_node.f)
  
  util.raiseNotDefined()


  
# Abbreviations
bfs = breadthFirstSearch
dfs = depthFirstSearch
astar = aStarSearch
ucs = uniformCostSearch