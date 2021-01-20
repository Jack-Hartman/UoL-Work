#include <iostream>
#include <string>
#include <cstdio>

using namespace std;

class Item {
	friend class Order;
	friend class Menu;
	protected:
		string calories;
		string name;
		double price = 0;

	public:
		virtual string toString() {
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal";
		}
};

class Appetiser : public Item {
	friend class Order;
	private:
		bool shareable;
		bool twoForOne;

	public: 
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
			string share, two;
			if (shareable) share = "(shareable) ";
			if (twoForOne) two = "(2-4-1)";
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal " + share + two;
		}
};

class MainCourse : public Item {
	public:
		MainCourse(string cal, string apName, string apPrice) {
			calories = cal;
			name = apName;
			price = stod(apPrice);
		}

		string type() {
			return "MainCourse";
		}
};

class Beverage : public Item {
	private:
		string abv;
		string volume;

	public:
		Beverage(string cal, string apName, string apPrice, string apAbv, string apVolume) {
			calories = cal;
			name = apName;
			price = stod(apPrice);
			abv = apAbv;
			volume = apVolume;
		}

		string toString() {
			char pricew[8];
			sprintf(pricew, "%.2f", price);
			return name + ": £" + pricew + ", " + calories + " cal ("+ volume + "ml, "+ abv + "% abv)";
		}
};
