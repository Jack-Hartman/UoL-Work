#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <sstream>     
#include <algorithm>

using namespace std;

class ItemList {
	protected:
		vector <Item*> items;
	public:
		
		virtual string toString() {
			return "";
		}
};


class Menu: public ItemList{
	friend class Order;
	public:
		Menu(string fileName) {
			ifstream file(fileName);
			if (!file) cout << "Error opening menu\n Please exit and try again.";
			else {
				string line;
				while (getline(file, line)) {
					string props[8];
					string prop;
					stringstream ssin(line);
					int i = 0;
					while (getline(ssin, prop, ',')) {
						props[i] = prop;
						i++;
					}

					// Create new item depending on category to instantiate with the correct class
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
			string type;

			vector <pair<int, Item*>> appetiers, mainCourses, beverages;
			for (Item* item : items) {
				if (dynamic_cast<Appetiser*>(item)) appetiers.emplace_back(i, item);
				else if (dynamic_cast<MainCourse*>(item)) mainCourses.emplace_back(i, item);
				else if (dynamic_cast<Beverage*>(item)) beverages.emplace_back(i,item);
				i++;
			}

			message += "----------------Appetisers----------------\n";
			std::sort(appetiers.begin(), appetiers.end(), [sort](const pair<int, Item*> li, const pair<int, Item*> ri) {
				if (sort == "asc") return li.second->price < li.second->price;
				else return li.second->price > li.second->price;
			});
			for (pair<int, Item*> item : appetiers) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}
			
			message += "----------------Main dishes----------------\n";
			std::sort(mainCourses.begin(), mainCourses.end(), [sort](const pair<int, Item*> li, const pair<int, Item*> ri) {
				if (sort == "asc") return li.second->price < li.second->price;
				else return li.second->price > li.second->price;
			});
			for (pair<int, Item*> item : mainCourses) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}

			message += "----------------Beverages----------------\n";
			std::sort(beverages.begin(), beverages.end(), [sort](const pair<int, Item*> li, const pair<int, Item*> ri) {
				if (sort == "asc") return li.second->price < li.second->price;
				else return li.second->price > li.second->price;
			});
			for (pair<int, Item*> item : beverages) {
				message += "(" + to_string(item.first) + ") " + item.second->toString() + "\n";
				string aa = "aa";
			}

			/*
				Before sorting

			for (Item* item : items) {
				string curType;
				if (dynamic_cast<Appetiser*>(item)) curType = "Appetiser";
				else if (dynamic_cast<MainCourse*>(item)) curType = "MainCourse";
				else if (dynamic_cast<Beverage*>(item)) curType = "Beverage";

				if (curType != type) {
					type = curType;
					if(curType == "Appetiser") message+= "----------------Appetisers----------------\n";
					else if (curType == "MainCourse") message += "----------------Main dishes----------------\n";
					else if (curType == "Beverage") message += "----------------Beverages----------------\n";
				}
				message += "(" + to_string(i) + ") " + item->toString() + "\n";
				i++;
			}
			*/
			return message +"\n";
		}
};