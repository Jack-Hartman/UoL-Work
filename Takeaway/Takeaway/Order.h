#include <algorithm>
#include <fstream>

using namespace std;

class Order: public ItemList{
	private: 
		double total;
	public:
		string calculateTotal() {
			double discount = 0;
			int twoForOne = 0;
			for (Item* item : items) {
				Appetiser* appetiser = dynamic_cast<Appetiser*>(item);
				if (appetiser && appetiser->shareable) {
					if (twoForOne == 1) {
						discount = + item->price;
						twoForOne = 0;
					}
					else twoForOne++;
				}
			}
			string totalText;
			if (discount > 0) {
				char discountw[8];
				sprintf(discountw, "%.2f", discount);
				totalText = "2-4-1 discount applied! Savings: £" + (string)discountw + "\n";
			}
			double endPrice = total - discount;
			char endPricew[8];
			sprintf(endPricew, "%.2f", endPrice);
			totalText += "Total: £" + (string)endPricew + "\n";
			return totalText;
		}

		void printReceipt() {
			ofstream receipt("receipt.txt");
			receipt << toString() + calculateTotal();
			receipt.close();

			//destory everything
		}

		string toString() {
			string receipt = "===== Checkout =====\n";
			int i = 1;
			for (Item* item : items) {
				receipt += "("+to_string(i)+ ") "+ item->toString() + "\n";
			}
			receipt += "-------------\n";
			receipt += calculateTotal();
			return receipt;
		}

		void add(vector<string> params, Menu menu) {
			try {
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
				if (itemsL.size() > 0) {
					for (int itemNo : itemsL) {
						Item* item = menu.items[itemNo];
						total += item->price;
						items.emplace_back(item);
						cout << item->name + " Added to order!\n";
					}
					cout << calculateTotal();
				}
				else cout << "Item provided not valid, please check menu using 'menu' command.\n";
			}
			catch(const std::exception& err)
			{
				cout << "Add only accepts numbers\n";
			}
		}

		void remove(vector<string> params) {
			if (items.size() > 0) {
				try {
					int max = items.size();
					vector<int> itemsL;
					for (string itemP : params) {
						int item = stoi(itemP);
						if (item <= max && item >= 1) itemsL.push_back(item-1);
						else {
							itemsL.clear();
							break;
						}
					}
					if (itemsL.size() > 0) {
						std::sort(itemsL.begin(), itemsL.end(), greater<int>());
						for (int item : itemsL) {
							total -= items[item]->price;
							items.erase((items.begin()+item));
						}
						cout << calculateTotal();
					}
					else cout << "Item provided not valid, please check basket using 'basket' command.\n";
				}
				catch (const std::exception& err)
				{
					cout << "Remove only accepts numbers\n";
				}
			}
			else {
				cout << "No items to remove";
			}
			
		}
};