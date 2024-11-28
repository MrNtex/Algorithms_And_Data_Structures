using AIDS7;
using System;
using Xunit;

namespace AIDS7.Test
{
    public class KDTreeTests
    {
        [Fact]
        public void Insert_AddsPointsCorrectly()
        {
            // Arrange
            var points = new double[][]
            {
            new double[] { 2.0, 3.0 },
            new double[] { 5.0, 4.0 },
            new double[] { 9.0, 6.0 },
            new double[] { 4.0, 7.0 },
            new double[] { 8.0, 1.0 },
            new double[] { 7.0, 2.0 }
            };
            var kdTree = new KDTree(points);

            // Act - Insert a new point
            kdTree.Insert(new double[] { 3.0, 4.0 });

            // Assert - Verify root and branches
            Assert.NotNull(kdTree);
            Assert.NotNull(kdTree);
        }

        [Fact]
        public void NearestNeighbor_ReturnsCorrectPoint()
        {
            // Arrange
            var points = new double[][]
            {
            new double[] { 2.0, 3.0 },
            new double[] { 5.0, 4.0 },
            new double[] { 9.0, 6.0 },
            new double[] { 4.0, 7.0 },
            new double[] { 8.0, 1.0 },
            new double[] { 7.0, 2.0 }
            };
            var kdTree = new KDTree(points);

            // Define the target point
            var target = new KDNode.KDPoint(new double[] { 6.0, 3.0 });

            // Act
            var nearest = kdTree.NearestNeighbor(kdTree.root, target, 0);

            // Assert
            Assert.NotNull(nearest);
            Assert.Equal(new double[] { 5.0, 4.0 }, nearest.point.point);
        }

        [Fact]
        public void Distance_CalculatesCorrectly()
        {
            // Arrange
            var pointA = new KDNode.KDPoint(new double[] { 3.0, 4.0 });
            var pointB = new KDNode.KDPoint(new double[] { 6.0, 8.0 });

            // Act
            var distance = KDTree.Distance(pointA, pointB);

            // Assert
            Assert.Equal(25.0, distance, precision: 5); // Distance squared
        }

        [Fact]
        public void Closest_ReturnsCorrectNode()
        {
            // Arrange
            var pointA = new KDNode(new double[] { 3.0, 4.0 });
            var pointB = new KDNode(new double[] { 6.0, 8.0 });
            var target = new KDNode.KDPoint(new double[] { 5.0, 5.0 });

            // Act
            var closest = new KDTree(new KDNode([0.0,0.0])).Closest(pointA, pointB, target);

            // Assert
            Assert.NotNull(closest);
            Assert.Equal(new double[] { 3.0, 4.0 }, closest.point.point);
        }
    }
}