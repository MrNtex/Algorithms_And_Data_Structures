import pandas as pd
import sympy as sp

# Read points from CSV
points = pd.read_csv('points.csv')

# Extract coordinates from the CSV
x_points = points['X'].values
y_points = points['Y'].values

# Define symbolic variables
x, y = sp.symbols('x y')

# Create the distance function
def f(x, y):
    total = 0
    for xi, yi in zip(x_points, y_points):
        # Euclidean distance to point (xi, yi)
        total += 1/(((x - xi) ** 2)+((y - yi) ** 2))
    return total

# Generate the symbolic function
distance_function = f(x, y)

# Convert to a string in Desmos-compatible format
desmos_function = sp.simplify(distance_function).evalf()
desmos_ready_function = sp.lambdify((x, y), desmos_function)

# Print the function for Desmos
print("Function for Desmos:")
print(str(distance_function))

initial_guess = sp.Matrix([0.5, 0.5])

# Optional: Compute derivative of the distance function if needed
df_dx = sp.diff(distance_function, x)
df_dy = sp.diff(distance_function, y)

print("Derivative with respect to x:")
print(df_dx)
print("Derivative with respect to y:")
print(df_dy)

# Solve the system of equations
numerical_solution = sp.nsolve([df_dx, df_dy], (x, y), initial_guess, tol=1e-4)
print("Numerical Solution:", numerical_solution)

# Filter for real solutions



# Output the solution
print("Critical Points:", numerical_solution)


df_dx_dx = sp.diff(df_dx, x)
df_dx_dy = sp.diff(df_dx, y)
df_dy_dx = sp.diff(df_dy, x)
df_dy_dy = sp.diff(df_dy, y)

print(df_dy_dx)
print(df_dx_dy)

print(df_dx_dy == df_dy_dx)  # Check if mixed partial derivatives are equal