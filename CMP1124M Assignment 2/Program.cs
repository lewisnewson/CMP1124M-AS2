using System;
using System.Collections.Generic;

namespace CMP1124M_Assignment_2
{
    // Class for holding the arrays to be used across the classes
    public static class Globals
    {
        // Load each of the resources into their designated array
        public static double[] Low_256 = FileSystem.load_resource(Properties.Resources.Low_256);
        public static double[] Low_2048 = FileSystem.load_resource(Properties.Resources.Low_2048);
        public static double[] Low_4096 = FileSystem.load_resource(Properties.Resources.Low_4096);
        public static double[] Mean_256 = FileSystem.load_resource(Properties.Resources.Mean_256);
        public static double[] Mean_2048 = FileSystem.load_resource(Properties.Resources.Mean_2048);
        public static double[] Mean_4096 = FileSystem.load_resource(Properties.Resources.Mean_4096);
        public static double[] High_256 = FileSystem.load_resource(Properties.Resources.High_256);
        public static double[] High_2048 = FileSystem.load_resource(Properties.Resources.High_2048);
        public static double[] High_4096 = FileSystem.load_resource(Properties.Resources.High_4096);
    }

    // Main class for the application
    class Program
    {
        // Entry point for the application
        static void Main(string[] args)
        {
            // Welcome welcome welcome
            Console.WriteLine("Please select a data set from the following:");
            Console.WriteLine("1 - Low 256");
            Console.WriteLine("2 - Mean 256");
            Console.WriteLine("3 - High 256");
            Console.Write("Please use the data set: ");
            // Record the user input, try to parse to an int, if it can't, keep asking
            int user_choice = Interaction.validate_choice(Console.ReadLine(), 3);
            // Clear the console
            Console.Clear();
            // Call out to the function to handle the sort request
            Interaction.Sorted_Arrays sorted_arrs = Interaction.run_that_sort(user_choice, false);
            // Wait for the user to press a key
            Console.Write(Environment.NewLine + "Press any key to continue onto searching! (insert cheer here)...");
            Console.ReadLine();
            Console.Clear();
            // Ask politely what value the user would hope to find in the array
            Console.Write("Alright bucko, what do you wish to find in the array: ");
            double user_search = Interaction.validate_search(Console.ReadLine());
            // Once the input has been validated, run the search functionality
            Searching.find_value(user_search, sorted_arrs.sorted_asc);  // <-- First iteration of code saw the use of a linear search over binary
            Searching.binary_search(user_search, sorted_arrs.sorted_asc);
            // Print out the efficiency calculations
            Console.WriteLine(Environment.NewLine + "Efficiency calculations:");
            Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.linear_search.type, Interaction.linear_search.operations);
            Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.binary_searching.type, Interaction.binary_searching.operations);
            Console.WriteLine(Environment.NewLine + "Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
            // Now we move onto the bigger files
            Console.WriteLine("Okay big boy, onto the bigger data sets:");
            Console.WriteLine("1 - Low 2048");
            Console.WriteLine("2 - Low 4096");
            Console.WriteLine("3 - Mean 2048");
            Console.WriteLine("4 - Mean 4096");
            Console.WriteLine("5 - High 2048");
            Console.WriteLine("6 - High 4096");
            Console.Write("Please use the data set: ");
            // Record the user input, try to parse to an int, if it can't, keep asking
            user_choice = Interaction.validate_choice(Console.ReadLine(), 6);
            // Clear the console
            Console.Clear();
            // Call out to the function to handle the sort request
            Interaction.run_that_sort(user_choice, true);
            // Wait for the user to press a key
            Console.Write(Environment.NewLine + "Press any key to continue onto searching! (insert cheer here)...");
            Console.ReadLine();
            Console.Clear();
            // Ask politely what value the user would hope to find in the array
            Console.Write("Alright pal, what do you wish to find in the array: ");
            user_search = Interaction.validate_search(Console.ReadLine());
            // Once the input has been validated, run the search functionality
            sorted_arrs = Interaction.run_that_sort(user_choice, true);
            // Reset back to 0
            Interaction.linear_search.operations = 0;
            Interaction.binary_searching.operations = 0;
            // Run the searching
            Searching.find_value(user_search, Interaction.which_array(user_choice, true));  // <-- First iteration of code saw the use of a linear search over binary
            Searching.binary_search(user_search, sorted_arrs.sorted_asc);
            // Print out the efficiency calculations
            Console.WriteLine(Environment.NewLine + "Efficiency calculations:");
            Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.linear_search.type, Interaction.linear_search.operations);
            Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.binary_searching.type, Interaction.binary_searching.operations);
            Console.WriteLine(Environment.NewLine + "Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
            // Loop 3 times to print out the merged results of each array size
            for (int data_set = 0; data_set < 3; data_set++)
            {
                // Run the sort on the merge of the arrays
                sorted_arrs = Interaction.run_direct_sort(Interaction.merge_arrays(data_set));
                // Run the search functionality
                Interaction.handle_search(sorted_arrs.sorted_asc);
                // Print out the efficiency calculations
                Console.WriteLine(Environment.NewLine + "Efficiency calculations:");
                Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.linear_search.type, Interaction.linear_search.operations);
                Console.WriteLine("The {0} task took {1} operations to complete.", Interaction.binary_searching.type, Interaction.binary_searching.operations);
                Console.WriteLine(Environment.NewLine + "Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            // Ask if they want to restart
            Console.Write("Well that draws us to the end of this demonstration, go again? (y/n)");
            if (Console.ReadLine() == "y")
            {
                // Clear the console
                Console.Clear();
                // Call the main function to run again
                Main(null);
            }
            // Close the program down
            Console.Clear();
            Console.WriteLine("Well on that disapointing bombshell...goodnight!");
            System.Threading.Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }

    // Class for handling all requests to the project resources
    class FileSystem
    {
        // Load in the specified resource file, returning an array of integers
        public static double[] load_resource(string resource)
        {
            // Use a try catch method to load the resource into an array
            try
            {
                // Split the resource file into an array of strings
                string[] array_ver = resource.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                // Define a list to be added to during the following iteration over the new array
                List<double> formatted_list = new List<double>();
                // Loop over each element in the array
                foreach (string element in array_ver)
                {
                    // Try parse the element to a double
                    double number;
                    if (Double.TryParse(element, out number))
                    {
                        // If it can be successfully parsed, add it to the list
                        formatted_list.Add(number);
                    }
                }
                // Convert the list back into an array of doubles
                double[] return_arr = formatted_list.ToArray();
                // Return the array
                return return_arr;
            }
            // If we encounter an error, print it to console for debugging
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadLine();
                Environment.Exit(0);
                return null;
            }
        }
    }

    // Class for handling user interaction such as validating inputs
    class Interaction
    {
        // Struct for holding the amount of operations a task takes
        public struct Efficiency
        {
            public string type;
            public int operations;
        }
        // Make some structs for holding efficiency numbers
        public static Efficiency bubble_sort_asc = new Efficiency();
        public static Efficiency bubble_sort_dsc = new Efficiency();
        public static Efficiency linear_search = new Efficiency();
        public static Efficiency binary_sort_asc = new Efficiency();
        public static Efficiency binary_sort_dsc = new Efficiency();
        public static Efficiency binary_searching = new Efficiency();


        // Validate the user input to make sure it's an integer and within the boundries
        public static int validate_choice(string raw, int max)
        {
            // Take in the user input as a string titled 'raw'
            int validated = 0;
            int input;
            // Try to parse the input first to an integer
            while ((Int32.TryParse(raw, out input) == false) || (input > max) || (input < 1))
            {
                // Tell the user off for trying to be clever and take their next input
                Console.Write("Cheeky, that's not a valid input...try again: ");
                raw = Console.ReadLine();
            }
            validated = input;
            // Return the validated input
            return validated;
        }

        // Validate the input for searching through the array with
        public static double validate_search(string raw)
        {
            // Take in the user input as a string titled 'raw'
            double validated = 0;
            double input;
            // Try to parse the input first to an integer
            while (Double.TryParse(raw, out input) == false)
            {
                // Tell the user off for trying to be clever and take their next input
                Console.Write("Cheeky, that's not a valid input...try again: ");
                raw = Console.ReadLine();
            }
            validated = input;
            // Return the validated input
            return validated;
        }

        // Create a struct to returnt he sorted arrays
        public struct Sorted_Arrays
        {
            // Store 2 arrays, one for ascending order and one for descending order
            public double[] sorted_asc;
            public double[] sorted_dsc;
        }

        // Take the user input and run the appropriate sort function
        public static Sorted_Arrays run_that_sort(int choice, bool big)
        {
            // Return the sorted arrays through a struct
            Sorted_Arrays return_vals = new Sorted_Arrays();
            // Firstly, congratulate the user on a good choice
            Console.WriteLine("Good choice, here are the outputs:");
            // Run through a switch case to see which array we need to target
            double[] target = which_array(choice, big);
            // Check to see what iterations we need to print out on
            int iterations = Sorting.arr_size(target);
            // Make a call to the sorting class to run a binary in order traversal
            Sorting.bubble_asc(target, false);  // <-- First iteration of code saw the use of a bubble sort over binary
            BST.Tree_Info bst_tree = BST.init_tree(target);
            BST.Tree binary_tree = bst_tree.binary_tree;
            // Clear the values currently held
            Sorting.sorted_array_asc.Clear();
            Sorting.sorted_array_dsc.Clear();
            Console.WriteLine(Environment.NewLine + "Ascending order {0}th value:", iterations);
            // Sort the incoming array using a binary tree structure
            Sorting.bst_in_order(bst_tree.curr_root);
            // Convert the returned list to an array
            double[] sorted_asc = Sorting.sorted_array_asc.ToArray();
            // Print the values we need to the console
            print_values(sorted_asc, iterations, big);
            return_vals.sorted_asc = sorted_asc;
            // Then make a call to the sorting class to run a binary  reverse in order traversal
            Sorting.bubble_dsc(target, false);  // <-- First iteration of code saw the use of a bubble sort over binary
            Console.WriteLine(Environment.NewLine + "Desending order {0}th value:", iterations);
            // Sort the incoming array using a binary tree structure
            Sorting.bst_reverse_in_order(bst_tree.curr_root);
            // Convert the returned list to an array
            double[] sorted_dsc = Sorting.sorted_array_dsc.ToArray();
            // Print the values we need to the console
            print_values(sorted_dsc, iterations, big);
            return_vals.sorted_dsc = sorted_dsc;
            // Return the efficiency numbers
            Console.WriteLine(Environment.NewLine + "Efficiency calculations:");
            Console.WriteLine("The {0} task took {1} operations to complete.", bubble_sort_asc.type, bubble_sort_asc.operations);
            Console.WriteLine("The {0} task took {1} operations to complete.", bubble_sort_dsc.type, bubble_sort_dsc.operations);
            Console.WriteLine("The {0} task took {1} operations to complete.", binary_sort_asc.type, binary_sort_asc.operations);
            Console.WriteLine("The {0} task took {1} operations to complete.", binary_sort_dsc.type, binary_sort_dsc.operations);
            // Return the sorted arrays
            return return_vals;
        }

        // Take in an array to sort instead of fetching it
        public static Sorted_Arrays run_direct_sort(double[] target)
        {
            // Make a call to the sorting class to run a bubble sort in ascending order
            // Sorting.bubble_asc(target, false);  // <- First version of code used the bubble sort method
            // Then make a call to the sorting class to run a bubble sort in ascending order
            // Sorting.bubble_dsc(target, false);  // <- First version of code used the bubble sort method

            // Store the arrays for returning
            Sorted_Arrays return_vals = new Sorted_Arrays();
            // We want to print out every 10th value
            int iterations = 10;
            // Make a call to the sorting class to run a binary in order traversal
            BST.Tree_Info direct_bst_tree = BST.init_tree(target);
            BST.Tree binary_tree = direct_bst_tree.binary_tree;
            // Clear the values currently held in the lists
            Sorting.sorted_array_asc.Clear();
            Sorting.sorted_array_dsc.Clear();
            Console.WriteLine(Environment.NewLine + "Ascending order {0}th value:", iterations);
            // Sort the incoming array using a binary tree structure
            Sorting.bst_in_order(direct_bst_tree.curr_root);
            // Convert the returned list to an array
            double[] sorted_asc = Sorting.sorted_array_asc.ToArray();
            // Print the values we need to the console
            print_values(sorted_asc, iterations, false);
            return_vals.sorted_asc = sorted_asc;
            Console.WriteLine(Environment.NewLine + "Desending order {0}th value:", iterations);
            // Sort the incoming array using a binary tree structure
            Sorting.bst_reverse_in_order(direct_bst_tree.curr_root);
            // Convert the returned list to an array
            double[] sorted_dsc = Sorting.sorted_array_dsc.ToArray();
            // Print the values we need to the console
            print_values(sorted_dsc, iterations, false);
            return_vals.sorted_dsc = sorted_dsc;
            return return_vals;
        }

        // Print the values we need to the console
        public static void print_values(double[] selected_arr, int iterations, bool big)
        {
            // Are we looking at the bigger datasets?
            if (big)
            {
                // In that case, only print out the value found once instead of every iteration
                Console.WriteLine("At index {0} we have {1}", iterations, selected_arr[iterations]);
            }
            // Otherwise
            else
            {
                // Loop over the sorted array and output the values we need
                for (int i = 0; i < selected_arr.Length; i += iterations)
                {
                    // Print out the values seen at every 10 place
                    Console.WriteLine("At index {0} we have {1}", i, selected_arr[i]);
                }
            }
        }

        // Return the array at the of the given choice
        public static double[] which_array(int choice, bool big)
        {
            // Run through a switch case to see which array we need to return
            double[] the_chosen_one = null;
            // Are we looking at the bigger data sets?
            if (big == false)
            {
                // Return the smaller data sets
                switch (choice)
                {
                    case 1:
                        the_chosen_one = Globals.Low_256;
                        break;
                    case 2:
                        the_chosen_one = Globals.Mean_256;
                        break;
                    case 3:
                        the_chosen_one = Globals.High_256;
                        break;
                }
            }
            else
            {
                // Return the bigger data sets
                switch (choice)
                {
                    case 1:
                        the_chosen_one = Globals.Low_2048;
                        break;
                    case 2:
                        the_chosen_one = Globals.Low_4096;
                        break;
                    case 3:
                        the_chosen_one = Globals.Mean_2048;
                        break;
                    case 4:
                        the_chosen_one = Globals.Mean_4096;
                        break;
                    case 5:
                        the_chosen_one = Globals.High_2048;
                        break;
                    case 6:
                        the_chosen_one = Globals.High_4096;
                        break;
                }
            }
            // Return the chosen array
            return the_chosen_one;
        }

        // Merge 2 given arrays using the given size
        public static double[] merge_arrays(int arr_size)
        {
            // Setup some storage for our selected array
            double[] upper_arr = null;
            double[] lower_arr = null;
            int size = 0;
            // Utilize a switch case to select our targeted arrays
            switch (arr_size)
            {
                case 0:
                    lower_arr = Globals.Low_256;
                    upper_arr = Globals.High_256;
                    size = 256;
                    break;
                case 1:
                    lower_arr = Globals.Low_2048;
                    upper_arr = Globals.High_2048;
                    size = 2048;
                    break;
                case 2:
                    lower_arr = Globals.Low_4096;
                    upper_arr = Globals.High_4096;
                    size = 4096;
                    break;
            }
            // See if we need to print out a message to the console
            Console.WriteLine("Output of the merge of the Low and High {0} arrays:", size);
            // Create a new array with the size of both arrays together
            double[] Merged_arr = new double[lower_arr.Length + upper_arr.Length];
            // Copy in the two arrays
            Array.Copy(lower_arr, Merged_arr, lower_arr.Length);
            Array.Copy(upper_arr, 0, Merged_arr, lower_arr.Length, upper_arr.Length);
            // Return the merged array
            return Merged_arr;
        }

        // Handle the searching for merged arrays
        public static void handle_search(double[] merged_arr)
        {
            // Wait for the user to press a key
            Console.Write(Environment.NewLine + "Press any key to continue onto searching! (insert cheer here)...");
            Console.ReadLine();
            Console.Clear();
            // Ask politely what value the user would hope to find in the array
            Console.Write("Alright chap, what do you wish to find in the array: ");
            double user_search = Interaction.validate_search(Console.ReadLine());
            // Set the efficieny counters to 0
            linear_search.operations = 0;
            binary_searching.operations = 0;
            // Once the input has been validated, run the search functionality
            Searching.find_value(user_search, merged_arr);
            Searching.binary_search(user_search, merged_arr);
        }
    }

    // Class for handling the different sorting functions
    class Sorting
    {
        // Create a list element for holding the sorted array
        public static List<double> sorted_array_asc = new List<double>();
        public static List<double> sorted_array_dsc = new List<double>();

        // Find the size of the given array
        public static int arr_size(double[] input_arr)
        {
            // Return the given increments depending on the size of the array passed in
            switch (input_arr.Length)
            {
                case 256:
                    return 10;
                case 2048:
                    return 50;
                case 4096:
                    return 80;
                default:
                    return 10;
            }
        }

        // Sort the given array into ascending order using the bubble sort method
        public static double[] bubble_asc(double[] input_arr, bool return_arr)
        {
            // Set the type for printing to console
            Interaction.bubble_sort_asc.type = "Bubble Sort in Ascending order";
            // Check what the size of the array is to determine what values we output
            int increments = arr_size(input_arr);
            // Console.WriteLine(Environment.NewLine + "Sorted in Ascending order, outputting every {0}th value from index of 0:", increments);
            // Initialize a temporary variable for when we start to swap values
            double temp = 0;
            // Begin doing a global loop over the entire array length
            for (int write = 0; write < input_arr.Length; write++)
            {
                // For each loop, loop again over the entire array to swap elements, should they need to be swapped
                for (int sort = 0; sort < input_arr.Length - 1; sort++)
                {
                    // If the current array index is greater than the next index
                    if (input_arr[sort] > input_arr[sort + 1])
                    {
                        // Assign the next index value to the temporary storage
                        temp = input_arr[sort + 1];
                        // Set the next index value to that of the current one
                        input_arr[sort + 1] = input_arr[sort];
                        // Set the current index value to the value stored in the temporary variable
                        input_arr[sort] = temp;
                        // Increment the operations for efficiency tracking
                        Interaction.bubble_sort_asc.operations++;
                    }
                }
            }
            // Loop over the sorted array and print out the elements found at every specified index
            for (int output = 0; output < input_arr.Length; output += increments)
            {
                // Print to the console the element found at that index in the array
                // Console.WriteLine("At index {0}, we have {1}", output, input_arr[output]);
            }
            // Do we want to return the sorted array somewhere?
            if (return_arr)
            {
                return input_arr;
            }
            // If not just return null
            else
            {
                return null;
            }
        }

        // Sort the given array into descending order using the bubble sort method
        public static double[] bubble_dsc(double[] input_arr, bool return_arr)
        {
            // Set the type for printing to console
            Interaction.bubble_sort_dsc.type = "Bubble Sort in Descending order";
            // Check what the size of the array is to determine what values we output
            int increments = arr_size(input_arr);
            // Console.WriteLine(Environment.NewLine + "Sorted in Descending order, outputting every {0}th value from index of 0:", increments);
            // Initialize a temporary variable for when we start to swap values
            double temp = 0;
            // Begin doing a global loop over the entire array length
            for (int write = 0; write < input_arr.Length; write++)
            {
                // For each loop, loop again over the entire array to swap elements, should they need to be swapped
                for (int sort = 1; sort < input_arr.Length; sort++)
                {
                    // If the current array index is greater than the previous index
                    if (input_arr[sort] > input_arr[sort - 1])
                    {
                        // Assign the previous index value to the temporary storage
                        temp = input_arr[sort - 1];
                        // Set the previous index value to that of the current one
                        input_arr[sort - 1] = input_arr[sort];
                        // Set the current index value to the value stored in the temporary variable
                        input_arr[sort] = temp;
                        Interaction.bubble_sort_dsc.operations++;
                    }
                }
            }
            // Loop over the sorted array and print out the elements found at every specified index
            for (int output = 0; output < input_arr.Length; output += increments)
            {
                // Print to the console the element found at that index in the array
                // Console.WriteLine("At index {0}, we have {1}", output, input_arr[output]);
            }
            // Do we want to return the sorted array somewhere?
            if (return_arr)
            {
                return input_arr;
            }
            // If not just return null
            else
            {
                return null;
            }
        }

        // In order traversal for the binary tree
        public static void bst_in_order(BST.Node root)
        {
            // Set the type of sorting
            Interaction.binary_sort_asc.type = "Binary Sort in Ascending order";
            // If we are at the end
            if (root == null)
            {
                // Return null
                return;
            }
            // Follow down the left path of the node
            bst_in_order(root.left);
            // Add the value to the list to be returned
            sorted_array_asc.Add(root.value);
            // Increment the interation counter
            Interaction.binary_sort_asc.operations++;
            // Then traverse down the right side of the root
            bst_in_order(root.right);
        }

        // In order traversal for the binary tree
        public static void bst_reverse_in_order(BST.Node root)
        {
            // Set the type of sorting
            Interaction.binary_sort_dsc.type = "Binary Sort in Descending order";
            // If we are at the end
            if (root == null)
            {
                // Return null
                return;
            }
            // Then traverse down the right side of the root
            bst_reverse_in_order(root.right);
            // Add the value to the list to be returned
            sorted_array_dsc.Add(root.value);
            // Increment the interation counter
            Interaction.binary_sort_dsc.operations++;
            // Follow down the left path of the node
            bst_reverse_in_order(root.left);
        }
    }

    // Class for handling the searching functionality
    class Searching
    {
        // Linear search for specified value
        public static void find_value(double request, double[] in_array)
        {
            // Set the search type
            Interaction.linear_search.type = "Linear search";
            // Declare a list for storing the locations it was found at
            List<int> indexes = new List<int>();
            // Declare a boolean to catch wheter we found the number or not
            bool found = false;
            // Count the operations needed
            // Loop over the entire array in search for the golden number
            for (int position = 0; position < in_array.Length - 1; position++)
            {
                // First check the element to see if we can find a direct match
                if (request == in_array[position])
                {
                    // We found it cap!
                    found = true;
                    // Add it to the indexes list for looping over in a sec
                    indexes.Add(position);
                }
                // If it's not found add one to the operations taken
                else
                {
                    // Increment the operations it has taken
                    Interaction.linear_search.operations++;
                }
            }
            // Inform the user the search is being done on a sorted array
            Console.WriteLine("The seach done over the sorted array in ascending order returned these results:");
            // If the number was found, print out the locations it was found at
            if (found)
            {
                // Conver the list to an array and loop over it
                int[] indexes_arr = indexes.ToArray();
                foreach (int index in indexes_arr)
                {
                    // Print to console the position the number was found at
                    Console.WriteLine("Found ya number at index {0}", index);
                }
            }
            // If we couldn't find the exact number, find the closest one
            else
            {
                int closest_index = 0;
                double closest_value = 0;
                double difference = 0;
                // Loop over the array
                for (int itt = 0; itt < in_array.Length - 1; itt++)
                {
                    // Work out the difference between the two numbers
                    double diff = request - in_array[itt];
                    // If the result is less than 0, multiply it up to keep things easy
                    if (diff < 0)
                    {
                        diff *= -1;
                    }
                    // Is this difference smaller than the one we have? (is it closer to the requested value)
                    if ((diff < difference) || (difference == 0))
                    {
                        // If it is, then set our variables accordingly
                        closest_index = itt;
                        closest_value = in_array[itt];
                        difference = diff;
                    }
                }
                // Let the user down lightly
                Console.WriteLine("Sorry mate, couldn't find your num. But {0} at index {1} is pretty close.", closest_value, closest_index);
                Console.Write(Environment.NewLine + "Alright mate no worries, thanks for looking.");
            }
        }

        // Struct for holding info about the closest nodes in case of the request not being found
        struct Unfound_Replacement
        {
            // Hold a value and an index
            public double value;
            public int index;
        }

        // Binary search alternative
        public static void binary_search(double request, double[] in_array)
        {
            // Set the search type
            Interaction.binary_searching.type = "Binary search";
            // Clear the console (keep it clean)
            Console.Clear();
            // Have we found the request the user was looking for?
            bool found = false;
            // Upper and lower bounds of our search params
            int lower_bound = 0;
            int upper_bound = in_array.Length;
            // Hold the closest numbers both smaller and bigger
            Unfound_Replacement closest_smaller = new Unfound_Replacement();
            Unfound_Replacement closest_bigger = new Unfound_Replacement();
            // As it says below (while our binary search is still active)
            while (lower_bound < upper_bound)
            {
                // Calculate the mid point
                int mid = (lower_bound + upper_bound) / 2;
                // If this is our request, nice!
                if (request == in_array[mid])
                {
                    // Write it to the console
                    Console.WriteLine("Found {0} at index {1} of the array, sorted in ascending order.", request, mid);
                    found = true;
                    // Save the current index for the mid point
                    int index = mid;
                    // If the element to the left of the current index also matches
                    if (in_array[mid - 1] == request)
                    {
                        // Begin a while loop to iterate over every instance found
                        while (in_array[index - 1] == request)
                        {
                            // Subtract 1 from the index value
                            index--;
                            // Print out that the value was also found at this location
                            Console.WriteLine("Also found {0} at index {1} of the array, sorted in ascending order.", request, index);
                            // Increment the operations by 1
                            Interaction.binary_searching.operations++;
                        }
                    }
                    // Reset the index to the midpoint again
                    index = mid;
                    // If the element to the right of the current index also matches
                    if (in_array[mid + 1] == request)
                    {
                        // Begin a while loop to iterate over every instance found
                        while (in_array[index + 1] == request)
                        {
                            // Increment 1 to the index value
                            index++;
                            // Print out that the value was also found at this location
                            Console.WriteLine("Also found {0} at index {1} of the array, sorted in ascending order.", request, index);
                            // Increment the operations by 1
                            Interaction.binary_searching.operations++;
                        }
                    }
                    // Break ot of the loop
                    break;
                }
                // If the request is left than this position
                else if (request < in_array[mid])
                {
                    // Adjust the boundries
                    upper_bound = mid - 1;
                    // Save the details of this node in case this is the last iteration
                    closest_bigger.value = in_array[mid];
                    closest_bigger.index = mid;
                }
                // Otherwise if it's bigger
                else
                {
                    // Adjust the boundries again
                    lower_bound = mid + 1;
                    // Save the details of this node in case this is the last iteration
                    closest_smaller.value = in_array[mid];
                    closest_smaller.index = mid;
                }
                // Iterate the amount of operations
                Interaction.binary_searching.operations++;
            }
            // If the request was not found
            if (!found)
            {
                // Work out the difference between the request and the closest smaller number
                double diff_sm = request - closest_smaller.value;
                // If the result is less than 0, multiply it up to keep things easy
                if (diff_sm < 0)
                {
                    diff_sm *= -1;
                }
                // Work out the difference between the request and the closest bigger number
                double diff_bg = request - closest_bigger.value;
                // If the result is less than 0, multiply it up to keep things easy
                if (diff_bg < 0)
                {
                    diff_bg *= -1;
                }
                //  Work out which difference is smaller
                if (diff_bg < diff_sm)
                {
                     // If the the bigger number is closer, print that out
                    Console.WriteLine("Couldn't find your {3}, but {0} at index {1} is only {2} away.", closest_bigger.value, closest_bigger.index, diff_bg, request);
                }
                else
                {
                    // Otherwise print out the smaller number
                    Console.WriteLine("Couldn't find your {3}, but {0} at index {1} is only {2} away.", closest_smaller.value, closest_smaller.index, diff_sm, request);
                }
                // Wait for the user's input before continuing
                Console.WriteLine(Environment.NewLine + "No worries mate, thanks for looking.");
            }
        }
    }

    // Bolt-on class for handling searching and sorting through the use of a binary tree
    class BST
    {
        // Create a struct for returning some data
        public struct Tree_Info
        {
            public Tree binary_tree;
            public Node curr_root;
        }

        // Initialize the binary search tree
        public static Tree_Info init_tree(double[] target_arr)
        {
            // Create new instances of the root and tree
            Node root = null;
            Tree binary_tree = new Tree();
            // Loop over the data set and perform insersions into the tree
            for (int i = 0; i < target_arr.Length; i++)
            {
                root = binary_tree.insert(root, target_arr[i]);
            }
            // Return the information
            Tree_Info tree_data = new Tree_Info();
            tree_data.binary_tree = binary_tree;
            tree_data.curr_root = root;
            return tree_data;
        }

        // Node class to hold current value as well as left and right values
        public class Node
        {
            // A value for the node along with left and right nodes
            public double value;
            public Node left;
            public Node right;
        }

        // Tree class handling functions such as inserting and traversing
        public class Tree
        {
            // Inserting a value takes in a root value and a value to add
            public Node insert(Node root, double element)
            {
                // If it's the first element in
                if (root == null)
                {
                    // Create a new node and then assign it's value to the one passes in
                    root = new Node();
                    root.value = element;
                }
                // If the value is less than the root given
                else if (element < root.value)
                {
                    // Apply the value to the left side of the node
                    root.left = insert(root.left, element);
                }
                // Otherwise (in this case if it's greater)
                else
                {
                    // Apply the value to the right of the node
                    root.right = insert(root.right, element);
                }
                // Return back the root node given
                return root;
            }
        }
    }
}