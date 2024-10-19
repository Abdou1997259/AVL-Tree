// See https://aka.ms/new-console-template for more information
using AVL_Tree;

AvlTree tree = new AvlTree();
tree.Insert(14);
tree.Insert(17);
tree.Insert(11);
tree.Insert(7);
tree.Insert(53);
tree.Insert(4);
tree.Insert(13);
tree.Insert(12);
tree.Insert(8);
tree.Insert(60);
tree.Insert(19);
tree.Insert(16);
tree.Insert(20);
tree.LevelOrder();


Console.WriteLine("after Delete 8");
tree.Remove(8);
tree.LevelOrder();
Console.WriteLine("after Delte 7");
tree.Remove(7);
tree.LevelOrder();
Console.WriteLine("after Delte 11");
tree.Remove(11);

tree.LevelOrder();
Console.WriteLine($"Successor of 13 is {tree.FindSuccessor(13).Value} and the Predecessor {tree.FindPredecessor(13).Value}");
Console.WriteLine("after Delte 14");
tree.Remove(14);
tree.LevelOrder();
Console.WriteLine("after Delte 17");
tree.Remove(17);
tree.LevelOrder();
Console.ReadLine();
