
#include <assert.h>
#include <ostream>
#include "decision_tree.h"

extern int classification_index;

// build examples out of csv file 
bool build_examples(const char* filename, examples_cont& out)
{
	out.clear();
	ifstream file;

	file.open(filename, ios::in);
	while (!file.eof())
	{
		string line;
		getline(file, line);
		if (line.empty())
			continue;

		example_t example;
		stringstream ssline(line);
		string token;
		while (getline(ssline, token, ','))
			example.push_back(token);
		example.erase(example.begin()); // remove name 

		if (!example.empty())
			out.push_back(example);
	}

	example_t example = out.front();
	classification_index = example.size() - 1; // get classification column 

	file.close();
	return true;
}

// build attributes out of examples, where first example contain the attributes names.
bool build_attributes(examples_cont& examples, attributes_cont& out)
{
	out.clear(); 

	// get header
	example_t header = examples.front();
	examples.pop_front();

	// build attributes 
	for (auto iter = header.begin(); iter != header.end() - 1; iter++)
	{
		int index = iter - header.begin();

		attribute_t attribute;
		attribute.m_name = *iter;
		attribute.m_examples_col = index;
		auto iter2 = examples.begin(); iter2++;
		for ( ; iter2 != examples.end(); iter2++)
		{
			example_t example = *iter2;
			attribute.m_range.insert(example[index]);
		}

		out.push_back(attribute);

	}

	return true; 
}

// create examples subset where max attribute equale to Vk 
void examples_filter(const examples_cont& examples, attribute_t& attribute, string Vk, examples_cont& out)
{
	out = examples;

	struct pred 
	{
		int m_index; string m_name;
		pred(int index, string name) { m_index = index; m_name = name; }
		bool operator ()(example_t& self) { return self[m_index] != m_name; }
	};

	out.remove_if(pred(attribute.m_examples_col, Vk));
}

// create attributes subset, without attribute
void attributes_filter(const attribute_t& attribute, attributes_cont& attributes)
{
	for (auto iter = attributes.begin(); iter != attributes.end(); iter++)
	{
		if ((*iter).m_name == attribute.m_name)
		{
			attributes.erase(iter);
			return;
		}
	}

	assert(false);
}

// merge two trees
void tree_t::merge(string label, const tree_t& other)
{
	int maxkey = m_nodes.rbegin()->first;
	int label_index; 

	// add new nodes 
	for (auto iter = other.m_nodes.begin(); iter != other.m_nodes.end(); iter++)
	{
		auto node = *iter;
		if (iter == other.m_nodes.begin())
		{
			m_nodes[node.first + maxkey + 1] = node.second;
			label_index = node.first + maxkey + 1;
		}
		else 
			m_nodes[node.first + maxkey + 1] = node.second;
	}

	// add new edges 
	for (auto iter = other.m_edges.begin(); iter != other.m_edges.end(); iter++)
	{
		auto edge = *iter;

		child_t child; 
		child.m_index = edge.second.m_index + maxkey + 1;
		child.m_label = edge.second.m_label;

		m_edges.insert(pair<int, child_t>(edge.first + maxkey + 1, child));
	}

	// add merge edge 
	child_t child;
	child.m_index = label_index;
	child.m_label = label;
	m_edges.insert(pair<int, child_t>(0, child));

}

// print tree
void tree_t::dump()
{
	cout << m_nodes[0] << endl;
	for (auto iter = m_edges.begin(); iter != m_edges.end(); iter++)
	{
		int first = iter->first;
		int second = iter->second.m_index;
		string label = iter->second.m_label;

		stringstream edge;
		edge.flags(ios_base::right);
		edge.width(2);
		edge << first << ':';  
		edge.flags(ios_base::left);
		edge.width(10);
		edge << m_nodes[first];
		edge << " -> ";
		edge.flags(ios_base::right);
		edge.width(2);
		edge << second << ':';
		edge.flags(ios_base::left);
		edge.width(10);
		edge << m_nodes[second];
		edge << " [label = " + label + "]";
		cout << edge.str() << endl;
	}
}

// count number of examples, where column 'index' equale to 'value'  
float count_examples(const examples_cont& examples, int index, string value)
{
	float retval = 0;
	for (auto iter = examples.begin(); iter != examples.end(); iter++)
		if ((*iter)[index] == value)
			retval++;
	return retval;
}

// count number of examples, where column 'index1' equale to 'value1' and column 'index2' equale to 'value2'
float count_examples(const examples_cont& examples, int index1, string value1, int index2, string value2)
{
	float retval = 0;
	for (auto iter = examples.begin(); iter != examples.end(); iter++)
		if ((*iter)[index1] == value1 && (*iter)[index2] == value2)
			retval++;
	return retval;
}

