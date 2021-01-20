/* ------------------------------------------------------
CMP2801M: Advanced Programming
Driver program for "Takeaway" assignment
Autumn 2020

Written by James Brown

This file is a representative test file.
During marking, we will use the exact same notation
as provided in the brief, so make sure you follow that guideline.
Also make sure that you don't modify the code provided here,
but instead add what you need to complete the tasks.

Good luck!
------------------------------------------------------ */

#define _CRT_SECURE_NO_WARNINGS

#include "Item.h"
#include "Menu.h"
#include "Order.h"

#include <iostream>
#include <vector>
#include <cstring>
#include <string>

using namespace std;

int main()
{
	string userCommand;
	vector <string> parameters;

	// Create a menu object from a CSV file
	Menu menu = Menu("menu.csv");

	// Create an order object
	Order order = Order();

	cout << "Welcome! Type 'help' to get started\n";
	while (userCommand != "exit")
	{
		getline(cin, userCommand);
		char* cstr = new char[userCommand.length() + 1];
		strcpy(cstr, userCommand.c_str());

		char * token;
		token = strtok(cstr, " ");

		while (token != NULL)
		{
			parameters.push_back(token);
			token = strtok(NULL, " ");
		}
		if (parameters.size() > 0) {
			string command = parameters[0];
			parameters.erase(parameters.begin()); // Remove command from parameters

			if (command.compare("menu") == 0) {
				string sort;
				if (parameters.size() > 0) {
					if (parameters[0] == "asc" || parameters[0] == "desc") sort = parameters[0];
				}
				else sort = "asc";
				if (sort != "") cout << menu.toString(sort);
				else cout << "Sorting order not recognised, please try again.\n";
			}
			else if (command.compare("add") == 0)
			{
				if (parameters.size() > 0) order.add(parameters, menu);
				else cout << "No item added to command\n";
			}
			else if (command.compare("remove") == 0)
			{
				if (parameters.size() > 0) order.remove(parameters);
				else cout << "No item added to command\n";
			}
			else if (command.compare("checkout") == 0)
			{
				cout << order.toString();
				bool complete = false;
				while (!complete) {
					cout << "\n\nDo you want to place your order? \nType'y', or 'n' to go back and modify it.\n";
					string resp; getline(cin, resp);
					if (resp == "y") {
						order.printReceipt();
						cout << "Order complete!\nReciept has been put into receipt.txt\n";
						userCommand = "exit";
						complete = true;
					}
					else if (resp == "n") {
						cout << "Checkout Cancelled\n\n";
						complete = true;
					}
					else cout << "Response not recognised, please try again.";
				}
			}
			else if (command.compare("help") == 0)
			{
				cout << "\nHelp:\nmenu - displays the menu\nadd [index] - adds item to order using number from menu e.g. add 1 3 2\nremove [index] - removes an item from order using number from menu e.g. remove 1 3 2\ncheckout - begins checkout of current order\nhelp - this menu of commands\nexit - terminates the program gracefully\n\n";
			}
			else
			{
				cout << "Command not found, type 'help' for more info\n";
			}
		}
		else {
			cout << "Command not found, type 'help' for more info\n";
		}
		

		parameters.clear();

	}

	cout << "Press any key to quit...";
	std::getchar();
}
