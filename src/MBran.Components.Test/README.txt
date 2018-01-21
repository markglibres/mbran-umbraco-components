=======================
COMPONENTS
=======================
- standard practice should be using compositions for doc types
- a single doctype for rendering
- View Rendering
	- @(Html.Component<{DocType}>(routeValues = null))
	- look for Views/Components/{DocType}.cshtml
	- map current page to {DocType}
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
	- @(Html.Component<{DocType}>(model, routeValues = null))
	- look for Views/Components/{DocType}.cshtml
	- map object model to {DocType}
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
	- @(Html.Component<{DocType}>(viewPath, routeValues = null))
	- look for views in [viewPath]
	- map current page to {DocType}
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
	- @(Html.Component<{DocType}>(viewPath, model, routeValues = null))
	- look for views in [viewPath]
	- map object model to {DocType}
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
	- @(Html.Component(model, routeValues = null))
	- look for Views/Components/{DocType}.cshtml
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
	- @(Html.Component(nodeId, routeValues = null))
	- look for Views/Components/{DocType}.cshtml
	- view model is of type {DocType}
	- optional params: RouteValueDictionary
	
- Controller Rendering
	- 2 options to create controller
	- A. Inherit from ComponentsController (inherits from SurfaceController)
		- access current model through property "Model" (model passed from view or currentpage)
		- can override GetViewModel() method. By default convert "Model" to a stronglyTyped model based on doctype
		- can override Render() method if you want to override viewPath location and model. this as the entry point of controller
		- access viewPath through property "ViewPath" (view path location passed from template)
		- view locations:
			=> custom view path if specified
			=> standard mvc view locations (controller / action name (Render action))
			=> if standard mvc view is not found, will look into /Views/Components/[DocType]
			
	- B. Create your own controller.
		- define your own routes or inherit from surfacecontroller
		- implement PartialViewResult Render() method as the entry point
		- access passed model through this.RouteData.Values[RouteDataConstants.ModelKey]
		- access component name through this.RouteData.Values[RouteDataConstants.ComponentTypeKey]
		- access viewPath through this.RouteData.Values[RouteDataConstants.ViewPathKey]

=======================
MODULES
=======================	
- execute controller  
- 2 options for controllers
- A. Inherit from ModulesController
	- access current model through property "Model" (model passed from view or currentpage if null)
	- standard mvc view location with "Render" as name of view
	- must define object GetViewModel() as the viewmodel to be passed to view
	- can override Render() method if you want to override viewPath location and model
	- access viewPath through property "ViewPath" (view path location passed from template)
	- view locations:
			=> custom view path if specified
			=> standard mvc view locations (controller / action name (Render action))
			=> if standard mvc view is not found, will look into /Views/Components/[Controller Name]

- B. Create your own controller.
	- define your own routes or inherit from surfacecontroller
	- must define PartialViewResult Render() as the entry point
	- use default MVC for view locations
	- access passed model through this.RouteData.Values[RouteDataConstants.ModelKey]
	- access viewPath through this.RouteData.Values[RouteDataConstants.ViewPathKey]
		
- @(Html.Module<MyController>(routeValues = null))
- optional params: RouteValueDictionary
- with Render() as entry point

- @(Html.Module<MyController>(model, routeValues = null))
- optional params: RouteValueDictionary
- with Render() as entry point
- with custom model passed

- @(Html.Module<MyController>(viewPath, routeValues = null))
- optional params: RouteValueDictionary
- with Render() as entry point
- with custom view path pased

- @(Html.Module<MyController>(viewPath, model, routeValues = null))
- optional params: RouteValueDictionary
- with Render() as entry point
- with custom model passed
- with custom view path pased

========================
COMPOSITIONS
========================
HasTextAndMedia
	- Title
	- Summary
	- Image
	
HasComponents
	- Components
	
HasMetaHeader
	- Title
	- Description

=======================
COMPONENTS
=======================
TextAndMediaLeft
	- HasTextAndMedia

TextAndMediaRight
	- HasTextAndMedia
=======================
PAGES
=======================
Home
	- HasTextAndMedia
	- HasComponents
	- HasMetaHeader
=======================
Content
=======================
Home 
	- Components =>
		- Text And Media Left
		- Text And Media Right
=======================
TEMPLATES
=======================

Home (~/Views/Home.cshtml)
	@inherits UmbracoViewPage<Home>
	@foreach (var component in Model.Components)
    {
        @Html.Component(component)
    }

TextAndMediaLeft (~/Views/Components/TextAndMediaLeft.cshtml)
	@model TextAndMediaLeft
	<div class="text-media-left">
		@(Html.Component<HasTextAndMedia>(Model))
	</div>

TextAndMediaRight (~/Views/Components/TextAndMediaRight.cshtml)
	@model TextAndMediaRight
	<div class="text-media-right">
		@(Html.Component<HasTextAndMedia>(Model))
	</div>
	
HasTextAndMedia (~/Views/Shared/HasTextAndMedia.cshtml)
	@model TextAndMediaRight
	@model HasTextAndMedia
	<h3>@Model.TextMediaTitle</h3>
	<div class="description">@Model.TextMediaSummary</div>
	<img src="@Model.TextMediaImage.Url" />