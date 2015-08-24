using System;

namespace ExtendedCollections.AVLTree
{
    public class AVLTreeNode<T> : IComparable<T> where T : IComparable
    {
        private AVLTreeNode<T> _left;
        private AVLTreeNode<T> _right;
        internal AVLTree<T> _tree;

        public AVLTreeNode(T value, AVLTreeNode<T> parent, AVLTree<T> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }
        public T Value { get; set; }

        public AVLTreeNode<T> Left
        {
            get { return _left; }
            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLTreeNode<T> Right
        {
            get { return _right; }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }
        public AVLTreeNode<T> Parent { get; internal set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }
        private void LeftRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> pivot = Right;

            bool isLeftChild = (rootParent != null) && rootParent.Left == root;
            root.Right = pivot.Left;
            pivot.Left = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.Right != null)
                root.Right.Parent = root;

            if (_tree.Head == root)
            {
                _tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.Left = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.Right = pivot;
            }
        }
        private void RightRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> pivot = Left;
            bool isLeftChild = (rootParent != null) && rootParent.Left == root;

            root.Left = pivot.Right;
            pivot.Right = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.Left != null)
                root.Left.Parent = root;

            if (_tree.Head == root)
            {
                _tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.Left = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.Right = pivot;
            }
        }
        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }
        private int MaxChildrenHeight(AVLTreeNode<T> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildrenHeight(node.Left), MaxChildrenHeight(node.Right));
            }
            return 0;
        }
        private int LeftHeight { get { return MaxChildrenHeight(Left); } }
        private int RightHeight { get { return MaxChildrenHeight(Right); } }
        public TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }
        private int BalanceFactor
        {
            get { return RightHeight - LeftHeight; }
        }
    }
}


