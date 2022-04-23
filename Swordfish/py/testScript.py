component = System.getComponent("Label", Entity0)
transform = System.getComponent("Transform", Entity0)
if component.Color.X > 1:
	component.Color = System.newVec3(0, component.Color.Y + 0.005,component.Color.Z + 0.002)
else: 
	if component.Color.Y > 1:
		component.Color = System.newVec3(component.Color.X + 0.001, 0,component.Color.Z + 0.002)
	else: 
		if component.Color.Z > 1:
			component.Color = System.newVec3(component.Color.X + 0.001, component.Color.Y + 0.005, 0)

transform.Rotation = System.newVec3(0, 0, transform.Rotation.Z + 0.1)
component.Text = "727"