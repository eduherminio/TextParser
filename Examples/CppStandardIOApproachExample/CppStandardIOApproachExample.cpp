// Either debug it from VS, run 'compile&run.bat' from VS command prompt or
// compile it as you'd usually do, passing as parameters "< SampleFiles/SimpleInput.txt > CPPVSCLISimpleOutput.txt"

// Process `SimpleInput.txt`, which contains an integer n(>= 0)
// followed by n doubles and a final string.

#include "stdafx.h"
#include <iostream>
#include <list>
#include <string>

int main()
{
	int _integer;
	std::string _str;
	std::list<double> _list;
	double _auxdouble;

	// Input start;
	std::cin >> _integer;
	for (int i = 0; i < _integer; ++i)
	{
		std::cin >> _auxdouble;
		_list.push_back(_auxdouble);
	}
	std::cin >> _str;
	// Input end

	// Data processing

	// Output start
	std::cout << _integer << " ";
	for (const double& d : _list)
		std::cout << d << " ";
	std::cout << _str;
	// Output end

	return 0;
}

