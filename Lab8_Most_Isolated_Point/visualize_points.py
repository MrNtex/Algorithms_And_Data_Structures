import numpy as np
import matplotlib.pyplot as plt
import tqdm


fileName = "grid_1000_1.txt"

# Load the CSV data
data = np.loadtxt(fileName, delimiter=",", dtype=int)

# Create the plot
plt.figure(figsize=(100, 100))
plt.imshow(data, cmap="tab10", origin="lower")  # Use 'tab10' colormap for distinct colors
plt.colorbar(label="Shop IDs")
plt.title("Shop Area Visualization")
plt.xlabel("X")
plt.ylabel("Y")

# Overlay numbers on the plot
for i in tqdm.tqdm(range(data.shape[0])):
    for j in range(data.shape[1]):
        if data[i, j] == -2:
            plt.text(j, i, "X", color="black", ha="center", va="center", fontsize=6)
            continue
        plt.text(j, i, str(data[i, j]), color="white", ha="center", va="center", fontsize=6)




plt.savefig(f"visalization_{fileName}.png", dpi=300)
plt.show()
