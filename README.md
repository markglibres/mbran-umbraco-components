# mbran-umbraco-components
MBran Components for Umbraco

```cs

@(Html.Component<MetaTagHeader>("routeData == null"))
@(Html.Component<MetaTagHeader>(Model,"routeData == null"))
@(Html.Component<MetaTagHeader>("~/Views/Components/Meta.cshtml","routeData == null"))
@(Html.Component<MetaTagHeader>("~/Views/Components/Meta.cshtml", Model,"routeData == null"))
@(Html.Component(Model,"routeData == null"))
@(Html.Component(nodeId,"routeData == null"))
@(Html.Component(componentType, Model,"routeData == null" ))
@(Html.Component(componentType, ~/Views/Components/Meta.cshtml", Model, "routeData == null" ))

@(Html.Module<EventsController>("routeData == null"))
@(Html.Module<EventsController>(Model,"routeData == null"))
@(Html.Module<EventsController>("~/Views/Components/Meta.cshtml","routeData == null"))
@(Html.Module<EventsController>("~/Views/Components/Meta.cshtml", Model,"routeData == null"))

```

This is a work in progress.. testing from api
