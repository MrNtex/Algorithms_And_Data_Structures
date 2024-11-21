import pandas as pd
import plotly.express as px
import os

script_dir = os.path.dirname(os.path.abspath(__file__))
file_path = os.path.join(script_dir, "results.csv")
data = pd.read_csv(file_path, delimiter=";")

data = data.dropna(subset=["Algorithm"])
data = data[data["Algorithm"].str.strip() != ""]

# Convert memory from bytes to MB
data["Memory (MB)"] = data["Memory"] / (1024 * 1024)

fig_time = px.scatter(
    data,
    x="DataSize",
    y="Time",
    color="Algorithm",
    title="Execution Time vs. Data Size",
    labels={"DataSize": "Data Size", "Time": "Execution Time (ms)"},
    log_x=True,
    hover_data=["Memory (MB)"]
)

fig_memory = px.scatter(
    data,
    x="DataSize",
    y="Memory (MB)",
    color="Algorithm",
    title="Memory Usage vs. Data Size",
    labels={"DataSize": "Data Size", "Memory (MB)": "Memory Usage (MB)"},
    log_x=True,
    hover_data=["Time"]
)

fig_time.show()
fig_memory.show()
