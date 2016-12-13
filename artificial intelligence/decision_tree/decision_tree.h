
#ifndef _DECISION_TREE_H_
#define _DECISION_TREE_H_

#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <list>
#include <vector>
#include <set>
#include <map>
using namespace std;

class attribute_t
{
public:
	string m_name; 
	int m_examples_col;
	set<string> m_range; 
	float m_gain;
};

class tree_t
{
public:
	typedef struct child_t
	{
		int m_index;
		string m_label;
	};
	map<int, string> m_nodes;
	multimap<int, child_t> m_edges;

	void merge(string label, const tree_t& other);
	void dump();
};

typedef vector<string> example_t;

typedef list<example_t> examples_cont;
typedef vector<attribute_t> attributes_cont;

bool build_examples(const char* filename, examples_cont& out);
bool build_attributes(examples_cont& examples, attributes_cont& out);
void examples_filter(const examples_cont& examples, attribute_t& attribute, string Vk, examples_cont& out);
void attributes_filter(const attribute_t& attribute, attributes_cont& attributes);
float count_examples(const examples_cont& examples, int index, string value);
float count_examples(const examples_cont& examples, int index1, string value1, int index2, string value2);

#endif // _DECISION_TREE_H_