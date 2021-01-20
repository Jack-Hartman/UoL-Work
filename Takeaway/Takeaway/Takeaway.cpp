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

	cout << "Welcome! Type 'help' to get started\n"; // welcome message
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
				// Check for sorting method
				string sort;
				if (parameters.size() > 0) {
					if (parameters[0] == "asc" || parameters[0] == "desc") sort = parameters[0];
					else sort = "invalid";
				}
				if (sort != "invalid") cout << menu.toString(sort); // call menu to string and output to console
				else cout << "Sorting order not recognised, please try again.\n"; // Error for unrecognised sorting format
			}
			else if (command.compare("add") == 0)
			{
				// Check if parameters are over one and call order.add() with menu and parameters which are the item locations in the order items vector
				if (parameters.size() > 0) order.add(parameters, menu);
				else cout << "No item added to command\n";
			}
			else if (command.compare("remove") == 0)
			{
				// Check if parameters are over one and call order.remove() with parameters which are the item locations in the order items vector
				if (parameters.size() > 0) order.remove(parameters);
				else cout << "No item added to command\n";
			}
			else if (command.compare("checkout") == 0)
			{
				// get order toString and see if it contains anything other than blank
				string checkout = order.toString("Checkout");
				if (checkout != "") {
					cout << checkout; // display checkout
					bool complete = false;
					while (!complete) { // Set while loop till one of the two expected answers is returned
						cout << "\n\nDo you want to place your order? \nType 'y', or 'n' to go back and modify it.\n"; 
						string resp; getline(cin, resp);
						if (resp == "y") {
							order.printReceipt(); // Prints reciept
							cout << "Order complete!\nReciept has been put into receipt.txt\n";
							userCommand = "exit"; // Set exit command to end while loop
							complete = true;
						}
						else if (resp == "n") {  
							cout << "Checkout Cancelled\n\n";
							complete = true;
						}
						else cout << "Response not recognised, please try again.\n";
					}
				}
				else cout << "No items in basket to checkout\n"; // Let the user know there isn't anything in the basket
			}
			else if (command.compare("help") == 0)
			{
				// Display help menu
				cout << "\nHelp:\nmenu [asc/desc | (optional)] - displays the menu\nbasket - view current items in order\nadd [index] - adds item to order using number from menu e.g. add 1 3 2\nremove [index] - removes an item from order using number from menu e.g. remove 1 3 2\ncheckout - begins checkout of current order\nhelp - this menu of commands\nexit - terminates the program gracefully\n\n";
			}
			else if (command.compare("basket") == 0) {
				// Display checkout layout without catch for input 
				string checkout = order.toString("Basket");
				if (checkout != "") cout << checkout + order.calculateTotal();
				else cout << "There are no items in the basket\n";
			}
			else if (command.compare("exit") == 0) cout << "Closing...\n"; 
			else
			{
				// If nothing is inputted display response
				cout << "Command not found, type 'help' for more info\n";
			}
		}
		else {
			cout << "Command not found, type 'help' for more info\n";
		}
		

		parameters.clear();

	}
	//Destructors called and close program
	menu.~Menu();
	order.~Order();
	cout << "Press any key to quit...";
	std::getchar();
}
