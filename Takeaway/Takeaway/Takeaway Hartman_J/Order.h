#include <algorithm>
#include <fstream>

using namespace std;

// Define Derived class Order
class Order: public ItemList{
	private: 
		double total;
	public:
		//Destructor, called when program exits
		virtual ~Order() {}

		string calculateTotal() {
			// Calculate discount by iterating through all items in order
			vector<double> discount;
			int twoForOne = 0;
			for (Item* &item : items) {
				Appetiser* appetiser = dynamic_cast<Appetiser*>(item); // If appetiser it returns the object if not it returns null
				if (appetiser && appetiser->twoForOne) { // Check if item is an appetiser and is eligable for twoforone
					discount.push_back(item->price);
					twoForOne++;
				}
			}
			//Apply discount and return formatted response for checkout, basket and order changes
			string totalText;
			double calcDiscount = 0;
			if (discount.size() > 0) {
				twoForOne = twoForOne / 2; // See how many fit into two
				std::sort(discount.begin(), discount.end()); // Sort into acending order
				for (int i = 0; i != twoForOne; i++) {
					calcDiscount += discount[i]; // Add lowest price to discount
				}
				char discountw[8];
				sprintf(discountw, "%.2f", calcDiscount);
				totalText = "2-4-1 discount applied! Savings: £" + (string)discountw + "\n";
			}
			double endPrice = total - calcDiscount; // Deduct discount from total
			char endPricew[8];
			sprintf(endPricew, "%.2f", endPrice);
			totalText += "Total: £" + (string)endPricew + "\n";
			return totalText;
		}

		void printReceipt() {
			// Write toString and calculatTotal to receipt.txt
			ofstream receipt("receipt.txt");
			receipt << toString("Checkout") + calculateTotal();
			receipt.close();
		}

		string toString(string func) {
			// Display checkout by iterating through order's items vector and calling each toString() then returning
			if (items.size() > 0) {
				string receipt = "===== " + func + " =====\n";
				int i = 1;
				for (Item* &item : items) {
					receipt += "(" + to_string(i) + ") " + item->toString() + "\n";
				}
				receipt += "-------------\n";
				receipt += calculateTotal();
				return receipt;
			}
			else return "";
		}

		void add(vector<string> params, Menu menu) {
			// try catch for checking if inputted values are integers
			try {
				// Check to see if input is within range of menu then push to int vector of items
				int max = menu.items.size();
				vector<int> itemsL;
				for (string itemP : params) {
					int item = stoi(itemP);
					if(item <= max && item >= 1) itemsL.push_back(item-1);
					else {
						itemsL.clear();
						break;
					}
				}
				// Check if there are any items added then iterate through item positions in menu and add them to order items
				if (itemsL.size() > 0) {
					for (int itemNo : itemsL) {
						Item* &item = menu.items[itemNo];
						total += item->price;
						items.emplace_back(item);
						cout << item->name + " Added to order!\n"; // Tell the user that the item has been added
					}
					cout << calculateTotal(); // Display total to user
				}
				else cout << "Item provided not valid, please check menu using 'menu' command.\n";
			}
			catch(const std::exception& err)
			{
				cout << "Add only accepts numbers\n";
			}
		}

		void remove(vector<string> params) {
			// check if there is any items in the basket
			if (items.size() > 0) {
				try { // try catch for checking if inputted values are integers
					int max = items.size();
					vector<int> itemsL;
					// Check to see if inputted integers are within range of the items in order if not clear and break
					for (string itemP : params) {
						int item = stoi(itemP);
						if (item <= max && item >= 1) itemsL.push_back(item-1);
						else {
							itemsL.clear();
							break;
						}
					}
					// Check if items are provided then sort into decending order, iterate through sorted array to remove at position provided
					if (itemsL.size() > 0) {
						std::sort(itemsL.begin(), itemsL.end(), greater<int>());
						for (int item : itemsL) {
							total -= items[item]->price;
							items.erase((items.begin()+item));
						}
						cout << calculateTotal(); // Recalculate total and display
					}
					else cout << "Item provided not valid, please check basket using 'basket' command.\n";
				}
				catch (const std::exception& err)
				{
					cout << "Remove only accepts numbers\n";
				}
			}
			else {
				cout << "Basket is empty\n";
			}
			
		}
};