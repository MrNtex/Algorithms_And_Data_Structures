class Solution(object):
    def recoverFromPreorder(self, traversal):
        """
        :type traversal: str
        :rtype: Optional[TreeNode]
        """
        if len(traversal) == 0:
            return None

        root = TreeNode(-1)
        stack = [root]

        def addNum(num, dashes):

            while dashes < len(stack)-1:
                stack.pop()

            if stack[-1].left:
                stack[-1].right = TreeNode(num)
                stack.append(stack[-1].right)
                return
            stack[-1].left = TreeNode(num)
            stack.append(stack[-1].left)

        dashes = 0
        num = -1
        for c in traversal:
            if c == '-':
                if num == -1:
                    dashes += 1
                else:
                    addNum(num, dashes)
                    dashes = 1
                    num = -1
            else:
                if num == -1:
                    num = int(c)
                else:
                    num *= 10
                    num += int(c)

        addNum(num, dashes)
        return root.left
