#include <iostream>
#include <string>
#include <cstdio>

using namespace std;

// Define abstract class item
class Item {
	friend class Order;
	friend class Menu;
	protected:
		string calories;
		string name;
		double price = 0;

	public:
		virtual string toString() {
			// returns item formated for menu with price set to 2 d.p
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal";
		}
};

// Define Derived class Appetiser
class Appetiser : public Item {
	friend class Order;
	private:
		bool shareable;
		bool twoForOne;

	public: 
		// Set Appetiser Constructor
		Appetiser(string cal, string apName, string apPrice, string share, string apTwoForOne) {
			calories = cal;
			name = apName;
			price = stod(apPrice);
			if (share == "y") shareable = true;
			else shareable = false;
			if (apTwoForOne == "y") twoForOne = true;
			else twoForOne = false;
		}

		string toString() {
			// returns item formated for menu with price set to 2 d.p with shareable and two for one formatting
			string share, two;
			if (shareable) share = "(shareable) ";
			if (twoForOne) two = "(2-4-1)";
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal " + share + two;
		}
};

// Define Derived class MainCourse
class MainCourse : public Item {
	public:
		// Set MainCourse Constructor
		MainCourse(string cal, string apName, string apPrice) {
			calories = cal;
			name = apName;
			price = stod(apPrice);
		}
};

// Define Derived class Beverage
class Beverage : public Item {
	private:
		string abv;
		string volume;

	public:
		// Set Beverage Constructor
		Beverage(string cal, string apName, string apPrice, string apAbv, string apVolume) {
			calories = cal;
			name = apName;
			price = stod(apPrice);
			abv = apAbv;
			volume = apVolume;
		}

		string toString() {
			// returns item formated for menu with price set to 2 d.p with colume and abv formatting
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal ("+ volume + "ml, "+ abv + "% abv)";
		}
};
