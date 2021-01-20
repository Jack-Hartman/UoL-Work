#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <sstream>     
#include <algorithm>

using namespace std;


// Define class ItemList
class ItemList {
	protected:
		vector <Item*> items;
	public:
		
		virtual string toString() {
			return "";
		}
};

// Define Derived class Menu
class Menu: public ItemList{
	friend class Order;
	public:
		//Destructor, called when program exits
		virtual ~Menu() {};

		// Menu Constructor to read file and add items from fileName into items vector
		Menu(string fileName) {
			ifstream file(fileName);
			if (!file) cout << "Error opening menu\n Please exit and try again.";
			else {
				string line;
				while (getline(file, line)) { // each line in file
					string props[8];
					string prop;
					stringstream ssin(line); // Stringstream set to allow getline to be used for a second time
					int i = 0;
					// Split each line into individual string seperated by ,
					while (getline(ssin, prop, ',')) {
						props[i] = prop;
						i++;
					}

					// Create new item depending on category to instantiate with the correct class then pushed to items vector using emplace_back to maintain polymorphism
					// 0 - Category, 1 - Name, 2 - Price, 3 - Calories, 4 - Shareable, 5 - TwoForOne, 6 - Volume, 7 - ABV
					if (props[0] == "a") {
						items.emplace_back(new Appetiser(props[3], props[1], props[2], props[4], props[5]));
					}
					else if (props[0] == "m") {
						items.emplace_back(new MainCourse(props[3], props[1], props[2]));
					}
					else if (props[0] == "b") {
						items.emplace_back(new Beverage(props[3], props[1], props[2], props[6], props[7]));
					}
				}
			}
			file.close();
		}

		string toString(string sort) {
			string message;
			int i = 1;

			// Define vectors appetiers, mainCourses, beverages with a paired integer and Item class
			vector <pair<int, Item*>> appetiers, mainCourses, beverages;

			// Iterate through menu and organise into seperate vectors with position in menu
			for (Item* &item : items) {
				if (dynamic_cast<Appetiser*>(item)) appetiers.emplace_back(make_pair(i, item));
				else if (dynamic_cast<MainCourse*>(item)) mainCourses.emplace_back(make_pair(i, item));
				else if (dynamic_cast<Beverage*>(item)) beverages.emplace_back(make_pair(i, item));
				i++;
			}

			// Sort appetisers and iterate through and add each item to messsage string
			message += "----------------Appetisers----------------\n";
			if (sort != "") {
				std::sort(appetiers.begin(), appetiers.end(), [sort](const pair<int, Item*> &li, const pair<int, Item*> &ri) {
					if (sort == "asc") return li.second->price < ri.second->price;
					else return li.second->price > ri.second->price;
					});
			}
			
			for (pair<int, Item*> &item : appetiers) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}
			
			// Sort mainCourses and iterate through and add each item to messsage string
			message += "----------------Main dishes----------------\n";
			if (sort != "") {
				std::sort(mainCourses.begin(), mainCourses.end(), [sort](const pair<int, Item*> &li, const pair<int, Item*> &ri) {
					if (sort == "asc") return li.second->price < ri.second->price;
					else return li.second->price > ri.second->price;
					});
			}
			for (pair<int, Item*> &item : mainCourses) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}

			// Sort beverages and iterate through and add each item to messsage string
			message += "----------------Beverages----------------\n";
			if (sort != "") {
				std::sort(beverages.begin(), beverages.end(), [sort](const pair<int, Item*> &li, const pair<int, Item*> &ri) {
					if (sort == "asc") return li.second->price < ri.second->price;
					else return li.second->price > ri.second->price;
					});
			}
			for (pair<int, Item*> &item : beverages) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}
			return message +"\n"; // return message
		}
};