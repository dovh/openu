
from planningProblem import PlanningProblem 
from planningProblem import maxLevel 
from planningProblem import levelSum
from search import aStarSearch
import time 

def createDomainFile(domainFileName, n):
  numbers = list(range(n)) # [0,...,n-1]
  pegs = ['a','b', 'c']
  domainFile = open(domainFileName, 'w') #use domainFile.write(str) to write to domainFile
  "*** YOUR CODE HERE ***"
  
  domainFile.write("Propositions:\n")

  index = 0
  for disk1 in numbers:
    line = ""
    index += 1
    for disk2 in numbers[index:]: 
      line += "d{0}d{1} ".format(disk1, disk2)
    line += "d{0}pa ".format(disk1)
    line += "d{0}pb ".format(disk1)
    line += "d{0}pc ".format(disk1)
    domainFile.write(line)

  domainFile.write("upa upb upc ")
  for disk in numbers:
    domainFile.write("ud{} ".format(disk))

  domainFile.write("\nActions:")

  index = 0
  for disk1 in numbers:
    index += 1
    for disk2 in numbers[index:]: 
      for disk3 in numbers[index:]: 
        if disk2==disk3: continue
        domainFile.write("\nName: Md{0}d{1}d{2} ".format(disk1, disk2, disk3))
        domainFile.write("\npre: d{0}d{1} ud{0} ud{2}".format(disk1, disk2, disk3))
        domainFile.write("\nadd: d{0}d{2} ud{1}".format(disk1, disk2, disk3))
        domainFile.write("\ndelete: d{0}d{1} ud{2} ".format(disk1, disk2, disk3))

  index = 0
  for disk1 in numbers:
    index += 1
    for disk2 in numbers[index:]: 
      for peg in pegs: 
        domainFile.write("\nName: Md{0}d{1}p{2} ".format(disk1, disk2, peg))
        domainFile.write("\npre: d{0}d{1} ud{0} up{2}".format(disk1, disk2, peg))
        domainFile.write("\nadd: d{0}p{2} ud{1}".format(disk1, disk2, peg))
        domainFile.write("\ndelete: d{0}d{1} up{2} ".format(disk1, disk2, peg))

  index = 0
  for disk1 in numbers:
    index += 1
    for peg in pegs: 
      for disk3 in numbers[index:]: 
        domainFile.write("\nName: Md{0}p{1}d{2} ".format(disk1, peg, disk3))
        domainFile.write("\npre: d{0}p{1} ud{0} ud{2}".format(disk1, peg, disk3))
        domainFile.write("\nadd: d{0}d{2} up{1}".format(disk1, peg, disk3))
        domainFile.write("\ndelete: d{0}p{1} ud{2} ".format(disk1, peg, disk3))

  index = 0
  for disk1 in numbers:
    index += 1
    for peg1 in pegs: 
      for peg2 in pegs: 
        if peg1==peg2: continue
        domainFile.write("\nName: Md{0}p{1}p{2} ".format(disk1, peg1, peg2))
        domainFile.write("\npre: d{0}p{1} ud{0} up{2}".format(disk1, peg1, peg2))
        domainFile.write("\nadd: d{0}p{2} up{1}".format(disk1, peg1, peg2))
        domainFile.write("\ndelete: d{0}p{1} up{2} ".format(disk1, peg1, peg2))

  domainFile.close()  
        
  
def createProblemFile(problemFileName, n):
  numbers = list(range(n)) # [0,...,n-1]
  pegs = ['a','b', 'c']
  problemFile = open(problemFileName, 'w') #use problemFile.write(str) to write to problemFile
  "*** YOUR CODE HERE ***"

  line = "ud0 "
  for disk in list(range(n-1)):
    line += "d{}d{} ".format(disk, disk+1)
  line += "d{0}pc upa upb".format(n-1)

  problemFile.write("Initial state: " + line)

  line = ""
  for disk in list(range(n-1)):
    line += "d{}d{} ".format(disk, disk+1)
  line += "d{0}pa ".format(n-1)
  problemFile.write("\nGoal state: " + line)

  problemFile.close()

import sys
if __name__ == '__main__':
  if len(sys.argv) != 2:
    print('Usage: hanoi.py n')
    sys.exit(2)
  
  n = int(float(sys.argv[1])) #number of disks
  domainFileName = 'hanoi' + str(n) + 'Domain.txt'
  problemFileName = 'hanoi' + str(n) + 'Problem.txt'
  
  createDomainFile(domainFileName, n)
  createProblemFile(problemFileName, n)

  #heuristic = levelSum #287
  heuristic = maxLevel #403
  #heuristic = lambda x,y: 0 #1507
  prob = PlanningProblem(domainFileName, problemFileName)
  start = time.clock()
  plan = aStarSearch(prob, heuristic)  
  elapsed = time.clock() - start
  if plan is not None:
    print("Plan found with %d actions in %.2f seconds" % (len(plan), elapsed))
    print('plan ' + str([act.getName() for act in plan if not act.isNoOp()]))
  else:
    print("Could not find a plan in %.2f seconds" %  elapsed)
  print("Search nodes expanded: %d" % prob._expanded)

