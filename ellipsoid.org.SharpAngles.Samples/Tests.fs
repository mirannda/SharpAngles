namespace ellipsoid.org.SharpAngles.Samples

open ellipsoid.org.SharpAngles
open ellipsoid.org.SharpAngles.Route
open ellipsoid.org.SharpAngles.UI
open IntelliFactory.WebSharper

[<JavaScript>]
module Tests =

    type TestRouteParams =
        { test: string }

    let TestsApp = 
        Angular.Module("testsApp", [| "ui.router" |])
               .Config(("$resource",
                            fun (resource: Resource.ResourceFactory<string>) -> 
                                ()
               ))
               .Config(("$routeProvider",
                            fun (routeProvider: RouteProvider) ->
                                routeProvider.When("/foo", RouteConfig(TemplateUrl = fun (p: TestRouteParams) -> sprintf "/foo/bar/%s" p.test))
                ))
               .Config(("$stateProvider", "$urlRouterProvider",
                            fun (stateProvider: StateProvider, urlRouterProvider: UrlRouterProvider) ->
                                stateProvider
                                    .State("state1", StateConfig(Url = "/state1", TemplateUrl = "partials/state1.html"))
                                        .State("state1.list", StateConfig(Url = "/list", TemplateUrl = "partials/state1.list.html"))
                                    .State("state2", StateConfig(Url = "/state2", TemplateUrl = "partials/state2.html"))
               ))
               .Config(("$urlMatcherFactory",
                            fun (urlMatcherFactory: UrlMatcherFactory) ->
                                urlMatcherFactory.StrictMode(false)
               ))
               .Controller("TestController",
                                ("$scope",
                                    fun (scope: Scope) ->
                                        scope.On("blah", fun x -> ())
               ))
               .Run(("$state", fun (state: StateService) -> state.TransitionTo("foo")))
               .Run(("$sce", fun (sce: SCEService) -> ()))
               .Run(("$q", fun (q: QService<string>) -> q.Defer ()))