=====================
- Umbraco workflow
	- Doc Type (page)
		- inheritance
		- compositions
	- Template (view)
		- render multiple partial views 
			- extensions or helpers for logic in views
			- logic within views
			- constant variables for view path
			- model mapping to viewmodels
	- Controllers 
		- custom controllers to handle logic for the page rendered
	- Problems:
		- cluttering, too many helpers, and extensions 
		- constants everywhere
		- no conventions
		
- Goal:
	- conventions on partial paths using default MVC logic
	- less constant variables esp for view paths
	- easy to debug
	- portable components (can be packaged)
	- quick way to add new components
	- minimize redundant code on mapping page to a view model 
			
- MBran Components
	- Concept:
		- Page (Doc type)
			- compositions over inheritance
			- consists of reusable components
		- Components
			- with own model
			- can have own logic
			- with own template
			- independent from page
			- auto routing of view paths
			- auto mapping of current page to component model (ModelsBuilder or any POCO)
		- Document Types
			- Composition 
				- starts with "_Has" 
				- i.e. _HasTextAndMedia, _HasMetaHeader
			- Component 
				- reusable
				- one single feature
				- i.e. TextAndMediaLeft, TextAndMediaRight
			- Page
				- can have multiple components
				- has multiple compositions
				- i.e. Home
					- components (nested content)
						- TextAndMediaLeft
						- TextAndMediaRight
					- _HasTextAndMedia
					- _HasMetaHeader
						
	- Implementation:
		- View Rendering
			- Render part of a page as a component model directly to a view
		- Controller Rendering
			- a view rendering
			- with custom logic on controller
		- Module Rendering
			- custom logic with own views
			- complex logic, i.e. one or more features involved
			
	- Examples:
	
	- Home
		- _HasMetaHeader 
			 - @(Html.Component<HasMetaHeader>())
				- View Rendering
					- Auto-map CurrentPage to HasMetaHeader model
					- pass generated model to "HasMetaHeader.cshtml".
						- will first try to look for the default MVC view routes
						- if not found, will look into ~/Views/Components/HasMetaHeader.cshtml
				- Controller Rendering 
					- Auto-map CurrentPage to HasMetaHeader model
					- If HasMetaHeaderController class (inherit from ComponentsController) exists
					- model generation can be overwritten by overriding "GetModel()" class and returns an object
					- model generated can be access through the property "Model".
					- has all features of SurfaceControllers (i.e. CurrentPage, etc)
					- override "Render()" to have control on rendering partial view and model object.
					
		- _HasComponents
			- TextAndMediaLeft
			- TextAndMediaRight
			- Loop through the nested content and render component:
				@inherits UmbracoViewPage<Home>
				@foreach (var component in Model.Components)
				{
					@Html.Component(component)
				}
				
		