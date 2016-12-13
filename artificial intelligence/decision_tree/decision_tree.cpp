// decision_tree.cpp : Defines the entry point for the console application.
//

#include "decision_tree.h"

int classification_index = -1;

// calc B(q) 
float B(float q)
{
	//result = -(q * log2f(q) + (1 - q)*log2f(1 - q));
	float result = 0;
	if (q != 0)
		result += -q * log2f(q);
	if (1 - q != 0)
		result += -(1 - q) * log2f(1 - q);

	return result;
}

// find maximum important attributes, by calculatin each attribute entropy 
//  and choosing the one with the highet energy gain 
attribute_t max_important(attributes_cont& attributes, const examples_cont& examples)
{
	// calculate original entroy 
	float p = count_examples(examples, classification_index, "+");
	float entropy = B(p / examples.size());

	for (auto iter = attributes.begin(); iter != attributes.end(); iter++)
	{
		attribute_t& attribute = *iter;

		// calculate attribute remanider 
		float remainder = 0;
		for (auto iter2 = attribute.m_range.begin(); iter2 != attribute.m_range.end(); iter2++)
		{
			string Vk = *iter2;
			float p = count_examples(examples, classification_index, "+", attribute.m_examples_col, Vk);
			float n = count_examples(examples, classification_index, "-", attribute.m_examples_col, Vk);

			int num = examples.size();
			if (((p + n) / num) != 0)
				remainder += ((p + n) / num) * B(p / (p + n));
		}

		// calculate attribute gain
		attribute.m_gain = entropy - remainder;
	}

	// choose maximum gain 
	float max_gain = 0;
	attribute_t max_attribute;
	for (auto iter = attributes.begin(); iter != attributes.end(); iter++)
	{
		if (max_gain < (*iter).m_gain)
		{
			max_gain = (*iter).m_gain;
			max_attribute = *iter;
		}
	}

	return max_attribute;
}

// decide true or false, by counting the number of classifications
tree_t plurality_value(const examples_cont& examples)
{
	float p = count_examples(examples, classification_index, "+");
	float n = count_examples(examples, classification_index, "-");

	tree_t tree;
	tree.m_nodes[0] = (n < p) ? "true" : "false";

	return tree;
}

/* implement DECISION_TREE_LEARNING algorithm (page 702, in vol 2)
/****************************************************/
tree_t decision_tree_learning(examples_cont examples, attributes_cont attributes, examples_cont parent_examples)
{
	// check if examples is empty
	if (examples.empty())
		return plurality_value(parent_examples);

	// check if all examples have the same classification 
	example_t header = examples.front();
	int  index = header.size() - 1;
	bool all_true = true;
	bool all_false = true;
	for (auto iter = examples.begin(); iter != examples.end(); iter++)
	{
		if ((*iter)[index] == "-") all_true = false;
		if ((*iter)[index] == "+") all_false = false;
	}
	if (all_true || all_false)
	{
		tree_t tree; 
		tree.m_nodes[0] = (all_true) ? "true" : "false";
		return tree;
	}
		
	// check if attributes are empty 
	if (attributes.empty())
		return plurality_value(examples);

	// find most important attribute
	attribute_t max_attribute = max_important(attributes, examples);

	// create attributes subset without max_attribute 
	attributes_cont attributes_subset = attributes;
	attributes_filter(max_attribute, attributes_subset);

	// create tree with only max_attribute as root
	tree_t tree; 
	tree.m_nodes[0] = max_attribute.m_name;

	for (auto iter = max_attribute.m_range.begin(); iter != max_attribute.m_range.end(); iter++)
	{
		// iterate on attribute range values 
		string Vk = *iter;

		// create examples subset where max attribute equale to Vk 
		examples_cont examples_subset;
		examples_filter(examples, max_attribute, Vk, examples_subset);

		// call recursion 
		tree_t subtree = decision_tree_learning(examples_subset, attributes_subset, examples);

		// add a branch to tree with label (attribute = Vk) and subtree 
		tree.merge(Vk, subtree);
	}

	return tree;
}

/* main
/****************************************************/
int main(int argc, char* argv[])
{
	if (argc != 2)
	{
		printf("usage: decision_tree.exe <filename.txt>");
		return -1; 
	}

	// reade examples 
	examples_cont examples;
	if (!build_examples(argv[1], examples))
	{
		printf("failed reading data file");
		return -1;
	}

	// read attributes from example (first line of examples contain the attrubutes names)
	attributes_cont attributes;
	if (!build_attributes(examples, attributes))
	{
		printf("failed building atributes");
		return -1;
	}
	
	// run DECISION_TREE_LEARNING algorithm 
	tree_t tree = decision_tree_learning(examples, attributes, examples);

	// print tree 
	tree.dump();       

	return 0;
}

