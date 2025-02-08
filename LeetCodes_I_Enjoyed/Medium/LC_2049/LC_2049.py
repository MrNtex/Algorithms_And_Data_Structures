class NumberContainers:
    def __init__(self):
        self.num_to_idx = defaultdict(list)
        self.idx_to_num = {}

    def change(self, index: int, number: int) -> None:
        self.idx_to_num[index] = number

        heapq.heappush(self.num_to_idx[number], index)

    def find(self, number: int) -> int:
        if not self.num_to_idx[number]:
            return -1

        while self.num_to_idx[number]:
            index = self.num_to_idx[number][0]
            if self.idx_to_num.get(index) == number:
                return index
            heapq.heappop(self.num_to_idx[number])
        return -1


# Your NumberContainers object will be instantiated and called as such:
# obj = NumberContainers()
# obj.change(index,number)
# param_2 = obj.find(number)