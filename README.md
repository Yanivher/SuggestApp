# SuggestApp

Data Structure:

Dictionary<string, Dictionary<string, int>>


Algorithm:

Each Word devided to it's sub strings with 0 index base. "ABC" will create 3 entries of "A", "AB" and "ABC".

Each Entry in the dictionary has another dictionary to hold the original word and it's whight (frequency).

After each insertion of the original word to the inner dictionary, it is sorted.


When searching for a search term all we need to do is to go in O(1) for the entry in the dictionary and get the 
list of words inside it in their current order.
