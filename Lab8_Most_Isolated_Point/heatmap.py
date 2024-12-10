from matplotlib.colors import LogNorm
import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

def generate_heatmap(csv_file_path, output_image_path):
    try:
        data = pd.read_csv(csv_file_path, header=None)
    except Exception as e:
        print(f"Error reading the CSV file: {e}")
        return

    heatmap_data = data.values
    heatmap_data = np.power(heatmap_data, 0.5) 
    plt.figure(figsize=(10, 8))
    sns.heatmap(heatmap_data, cmap="viridis", cbar=True, norm=LogNorm(vmin=heatmap_data.min(), vmax=.05))

    plt.title("Heatmap")
    plt.xlabel("X-axis")
    plt.ylabel("Y-axis")

    try:
        plt.savefig(output_image_path, dpi=300, bbox_inches="tight")
        print(f"Heatmap saved to {output_image_path}")
    except Exception as e:
        print(f"Error saving the heatmap image: {e}")

    plt.show()


csv_file = "heatmap_500.txt" 
output_image = "heatmap_sqrt_transform.png"  

generate_heatmap(csv_file, output_image)