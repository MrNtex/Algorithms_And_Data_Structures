using Xunit;

public class BSTTests
{
    [Fact]
    public void Add_ShouldInsertValuesInCorrectPosition()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);

        Assert.True(tree.Search(10));
        Assert.True(tree.Search(5));
        Assert.True(tree.Search(15));
    }

    [Fact]
    public void Remove_ShouldDeleteNodeWithoutChildren()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);
        tree.Remove(5);

        Assert.False(tree.Search(5));
    }

    [Fact]
    public void Remove_ShouldDeleteNodeWithOneChild()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);
        tree.Add(3);
        tree.Remove(5);

        Assert.False(tree.Search(5));
        Assert.True(tree.Search(3));
    }

    [Fact]
    public void Remove_ShouldDeleteNodeWithTwoChildren()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);
        tree.Add(3);
        tree.Add(7);
        tree.Remove(5);

        Assert.False(tree.Search(5));
        Assert.True(tree.Search(3));
        Assert.True(tree.Search(7));
    }

    [Fact]
    public void GetDepth_ShouldReturnCorrectDepth()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);
        tree.Add(3);
        tree.Add(7);

        int depth = tree.GetHeight();

        Assert.Equal(3, depth);
    }

    [Fact]
    public void GetSpan_ShouldReturnDifferenceBetweenMaxAndMinDepth()
    {
        var tree = new BST<int>();

        tree.Add(10);
        tree.Add(5);
        tree.Add(15);
        tree.Add(3);
        tree.Add(7);

        int span = tree.GetSpan();

        Assert.Equal(1, span);  // Max depth = 3, Min depth = 2, span = 1
    }
}
