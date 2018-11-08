using System.Web.Http;
using WebActivatorEx;
using UserCenter.OpenAPI;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http.Description;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Xml.Linq;
using System.Reflection;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace UserCenter.OpenAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        // By default, the service root url is inferred from the request used to access the docs.
                        // However, there may be situations (e.g. proxy and load-balanced environments) where this does not
                        // resolve correctly. You can workaround this by providing your own code to determine the root URL.
                        //
                        //c.RootUrl(req => GetRootUrlFromAppConfig());

                        // If schemes are not explicitly provided in a Swagger 2.0 document, then the scheme used to access
                        // the docs is taken as the default. If your API supports multiple schemes and you want to be explicit
                        // about them, you can use the "Schemes" option as shown below.
                        //
                        //c.Schemes(new[] { "http", "https" });

                        // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                        // hold additional metadata for an API. Version and title are required but you can also provide
                        // additional fields by chaining methods off SingleApiVersion.
                        //
                        //c.SingleApiVersion("v1", "UserCenter.OpenAPI");
                        c.IncludeXmlComments($"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/{typeof(SwaggerConfig).Assembly.GetName().Name}.XML");

                        // If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                        //
                        //c.PrettyPrint();

                        // If your API has multiple versions, use "MultipleApiVersions" instead of "SingleApiVersion".
                        // In this case, you must provide a lambda that tells Swashbuckle which actions should be
                        // included in the docs for a given API version. Like "SingleApiVersion", each call to "Version"
                        // returns an "Info" builder so you can provide additional metadata per API version.
                        //
                        //c.MultipleApiVersions(
                        //    (apiDesc, targetApiVersion) => ResolveVersionSupportByRouteConstraint(apiDesc, targetApiVersion),
                        //    (vc) =>
                        //    {
                        //        vc.Version("v2", "Swashbuckle Dummy API V2");
                        //        vc.Version("v1", "Swashbuckle Dummy API V1");
                        //    });
                        c.MultipleApiVersions(
                            (apiDesc, targetApiVersion) => ResolveVersionSupportByRouteConstraint(apiDesc, targetApiVersion),
                            (vc) =>
                            {
                                vc.Version("v1", "UserCenter.OpenAPI 版本 v1");
                                vc.Version("v2", "UserCenter.OpenAPI 版本 v2");
                                vc.Version("v3", "UserCenter.OpenAPI 版本 v3 JWT验证");
                            });

                        // You can use "BasicAuth", "ApiKey" or "OAuth2" options to describe security schemes for the API.
                        // See https://github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md for more details.
                        // NOTE: These only define the schemes and need to be coupled with a corresponding "security" property
                        // at the document or operation level to indicate which schemes are required for an operation. To do this,
                        // you'll need to implement a custom IDocumentFilter and/or IOperationFilter to set these properties
                        // according to your specific authorization implementation
                        //
                        //c.BasicAuth("basic")
                        //    .Description("Basic HTTP Authentication");
                        //
                        // NOTE: You must also configure 'EnableApiKeySupport' below in the SwaggerUI section
                        //c.ApiKey("apiKey")
                        //    .Description("API Key Authentication")
                        //    .Name("apiKey")
                        //    .In("header");
                        //
                        //c.OAuth2("oauth2")
                        //    .Description("OAuth2 Implicit Grant")
                        //    .Flow("implicit")
                        //    .AuthorizationUrl("http://petstore.swagger.wordnik.com/api/oauth/dialog")
                        //    //.TokenUrl("https://tempuri.org/token")
                        //    .Scopes(scopes =>
                        //    {
                        //        scopes.Add("read", "Read access to protected resources");
                        //        scopes.Add("write", "Write access to protected resources");
                        //    });

                        // Set this flag to omit descriptions for any actions decorated with the Obsolete attribute
                        //c.IgnoreObsoleteActions();

                        // Each operation be assigned one or more tags which are then used by consumers for various reasons.
                        // For example, the swagger-ui groups operations according to the first tag of each operation.
                        // By default, this will be controller name but you can use the "GroupActionsBy" option to
                        // override with any value.
                        //
                        //c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());

                        // You can also specify a custom sort order for groups (as defined by "GroupActionsBy") to dictate
                        // the order in which operations are listed. For example, if the default grouping is in place
                        // (controller name) and you specify a descending alphabetic sort order, then actions from a
                        // ProductsController will be listed before those from a CustomersController. This is typically
                        // used to customize the order of groupings in the swagger-ui.
                        //
                        //c.OrderActionGroupsBy(new DescendingAlphabeticComparer());

                        // If you annotate Controllers and API Types with
                        // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                        // those comments into the generated docs and UI. You can enable this by providing the path to one or
                        // more Xml comment files.
                        //
                        //c.IncludeXmlComments(GetXmlCommentsPath());

                        // Swashbuckle makes a best attempt at generating Swagger compliant JSON schemas for the various types
                        // exposed in your API. However, there may be occasions when more control of the output is needed.
                        // This is supported through the "MapType" and "SchemaFilter" options:
                        //
                        // Use the "MapType" option to override the Schema generation for a specific type.
                        // It should be noted that the resulting Schema will be placed "inline" for any applicable Operations.
                        // While Swagger 2.0 supports inline definitions for "all" Schema types, the swagger-ui tool does not.
                        // It expects "complex" Schemas to be defined separately and referenced. For this reason, you should only
                        // use the "MapType" option when the resulting Schema is a primitive or array type. If you need to alter a
                        // complex Schema, use a Schema filter.
                        //
                        //c.MapType<ProductType>(() => new Schema { type = "integer", format = "int32" });

                        // If you want to post-modify "complex" Schemas once they've been generated, across the board or for a
                        // specific type, you can wire up one or more Schema filters.
                        //
                        //c.SchemaFilter<ApplySchemaVendorExtensions>();

                        // In a Swagger 2.0 document, complex types are typically declared globally and referenced by unique
                        // Schema Id. By default, Swashbuckle does NOT use the full type name in Schema Ids. In most cases, this
                        // works well because it prevents the "implementation detail" of type namespaces from leaking into your
                        // Swagger docs and UI. However, if you have multiple types in your API with the same class name, you'll
                        // need to opt out of this behavior to avoid Schema Id conflicts.
                        //
                        //c.UseFullTypeNameInSchemaIds();

                        // Alternatively, you can provide your own custom strategy for inferring SchemaId's for
                        // describing "complex" types in your API.
                        //
                        //c.SchemaId(t => t.FullName.Contains('`') ? t.FullName.Substring(0, t.FullName.IndexOf('`')) : t.FullName);

                        // Set this flag to omit schema property descriptions for any type properties decorated with the
                        // Obsolete attribute
                        //c.IgnoreObsoleteProperties();

                        // In accordance with the built in JsonSerializer, Swashbuckle will, by default, describe enums as integers.
                        // You can change the serializer behavior by configuring the StringToEnumConverter globally or for a given
                        // enum type. Swashbuckle will honor this change out-of-the-box. However, if you use a different
                        // approach to serialize enums as strings, you can also force Swashbuckle to describe them as strings.
                        //
                        //c.DescribeAllEnumsAsStrings();

                        // Similar to Schema filters, Swashbuckle also supports Operation and Document filters:
                        //
                        // Post-modify Operation descriptions once they've been generated by wiring up one or more
                        // Operation filters.
                        //
                        //c.OperationFilter<AddDefaultResponse>();
                        //
                        // If you've defined an OAuth2 flow as described above, you could use a custom filter
                        // to inspect some attribute on each action and infer which (if any) OAuth2 scopes are required
                        // to execute the operation
                        //
                        //c.OperationFilter<AssignOAuth2SecurityRequirements>();

                        // Post-modify the entire Swagger document by wiring up one or more Document filters.
                        // This gives full control to modify the final SwaggerDocument. You should have a good understanding of
                        // the Swagger 2.0 spec. - https://github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md
                        // before using this option.
                        //
                        //c.DocumentFilter<ApplyDocumentVendorExtensions>();

                        // In contrast to WebApi, Swagger 2.0 does not include the query string component when mapping a URL
                        // to an action. As a result, Swashbuckle will raise an exception if it encounters multiple actions
                        // with the same path (sans query string) and HTTP method. You can workaround this by providing a
                        // custom strategy to pick a winner or merge the descriptions for the purposes of the Swagger docs
                        //
                        //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                        // Wrap the default SwaggerGenerator with additional behavior (e.g. caching) or provide an
                        // alternative implementation for ISwaggerProvider with the CustomProvider option.
                        //
                        c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                    })
                .EnableSwaggerUi(c =>
                    {
                        // Use the "DocumentTitle" option to change the Document title.
                        // Very helpful when you have multiple Swagger pages open, to tell them apart.
                        //
                        //c.DocumentTitle("My Swagger UI");
                        c.DocumentTitle("用户中心开发接口");

                        // Use the "InjectStylesheet" option to enrich the UI with one or more additional CSS stylesheets.
                        // The file must be included in your project as an "Embedded Resource", and then the resource's
                        // "Logical Name" is passed to the method as shown below.
                        //
                        //c.InjectStylesheet(containingAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testStyles1.css");

                        // Use the "InjectJavaScript" option to invoke one or more custom JavaScripts after the swagger-ui
                        // has loaded. The file must be included in your project as an "Embedded Resource", and then the resource's
                        // "Logical Name" is passed to the method as shown above.
                        //
                        //c.InjectJavaScript(thisAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testScript1.js");
                        // 使用中文
                        c.InjectJavaScript(thisAssembly, "UserCenter.OpenAPI.Content.Scripts.swagger_lang.js");

                        // The swagger-ui renders boolean data types as a dropdown. By default, it provides "true" and "false"
                        // strings as the possible choices. You can use this option to change these to something else,
                        // for example 0 and 1.
                        //
                        //c.BooleanValues(new[] { "0", "1" });

                        // By default, swagger-ui will validate specs against swagger.io's online validator and display the result
                        // in a badge at the bottom of the page. Use these options to set a different validator URL or to disable the
                        // feature entirely.
                        //c.SetValidatorUrl("http://localhost/validator");
                        //c.DisableValidator();

                        // Use this option to control how the Operation listing is displayed.
                        // It can be set to "None" (default), "List" (shows operations for each resource),
                        // or "Full" (fully expanded: shows operations and their details).
                        //
                        //c.DocExpansion(DocExpansion.List);

                        // Specify which HTTP operations will have the 'Try it out!' option. An empty paramter list disables
                        // it for all operations.
                        //
                        //c.SupportedSubmitMethods("GET", "HEAD");

                        // Use the CustomAsset option to provide your own version of assets used in the swagger-ui.
                        // It's typically used to instruct Swashbuckle to return your version instead of the default
                        // when a request is made for "index.html". As with all custom content, the file must be included
                        // in your project as an "Embedded Resource", and then the resource's "Logical Name" is passed to
                        // the method as shown below.
                        //
                        //c.CustomAsset("index", containingAssembly, "YourWebApiProject.SwaggerExtensions.index.html");

                        // If your API has multiple versions and you've applied the MultipleApiVersions setting
                        // as described above, you can also enable a select box in the swagger-ui, that displays
                        // a discovery URL for each version. This provides a convenient way for users to browse documentation
                        // for different API versions.
                        //
                        //c.EnableDiscoveryUrlSelector();

                        // If your API supports the OAuth2 Implicit flow, and you've described it correctly, according to
                        // the Swagger 2.0 specification, you can enable UI support as shown below.
                        //
                        //c.EnableOAuth2Support(
                        //    clientId: "test-client-id",
                        //    clientSecret: null,
                        //    realm: "test-realm",
                        //    appName: "Swagger UI"
                        //    //additionalQueryStringParams: new Dictionary<string, string>() { { "foo", "bar" } }
                        //);

                        // If your API supports ApiKey, you can override the default values.
                        // "apiKeyIn" can either be "query" or "header"
                        //
                        //c.EnableApiKeySupport("apiKey", "header");
                    });
        }


        static Regex version = new Regex(@"api/v\d+/", RegexOptions.IgnoreCase);
        private static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            var apiVersion = targetApiVersion.ToLower();
            if (apiVersion == "v1")
            {
                return apiDesc.RelativePath.Contains("/v1/")
                  || version.IsMatch(apiDesc.RelativePath) == false;
            }

            return apiDesc.RelativePath.Contains("/" + apiVersion + "/");
        }


    }


    public class CachingSwaggerProvider : ISwaggerProvider
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =
            new ConcurrentDictionary<string, SwaggerDocument>();

        private static Regex version = new Regex(@"api/v\d+/", RegexOptions.IgnoreCase);

        private readonly ISwaggerProvider _swaggerProvider;

        public CachingSwaggerProvider(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                SwaggerGenerator generator = _swaggerProvider as SwaggerGenerator;

                var apiExplorer = (IApiExplorer)generator.GetType().GetRuntimeFields().Where(f => f.Name == "_apiExplorer").First().GetValue(generator);
                var settings = (JsonSerializerSettings)generator.GetType().GetRuntimeFields().Where(f => f.Name == "_jsonSerializerSettings").First().GetValue(generator);
                var apiVersions = (IDictionary<string, Info>)generator.GetType().GetRuntimeFields().Where(f => f.Name == "_apiVersions").First().GetValue(generator);
                var options = (SwaggerGeneratorOptions)generator.GetType().GetRuntimeFields().Where(f => f.Name == "_options").First().GetValue(generator);

                var myGenerator = new MySwaggerGenerator(apiExplorer, settings, apiVersions, options);
                srcDoc = myGenerator.GetSwagger(rootUrl, apiVersion);
                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc(apiVersion) } };

                _cache.TryAdd(cacheKey, srcDoc);

            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        public static ConcurrentDictionary<string, string> GetControllerDesc(string apiVersion = "")
        {
            string xmlpath = $"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/{typeof(SwaggerConfig).Assembly.GetName().Name}.XML";
            Regex @namespace = new Regex(@"\.v\d+\.", RegexOptions.IgnoreCase);
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            if (File.Exists(xmlpath))
            {
                XDocument doc = XDocument.Load(xmlpath);
                var ctrlElement = doc.Descendants("member").Where(x => x.Attribute("name").Value.StartsWith("T:")&& x.Attribute("name").Value.EndsWith("Controller"));
                apiVersion = apiVersion.ToLower();
                if (apiVersion == "v1")
                {
                    //TestController
                    ctrlElement = ctrlElement.Where(x =>
                    {
                        var value = x.Attribute("name").Value;
                        return
                          value.Contains($".{apiVersion}.")
                      || !@namespace.IsMatch(value);
                    });
                }
                else
                {
                    ctrlElement = ctrlElement.Where(x =>
                    {
                        return
                          x.Attribute("name").Value.Contains($".{apiVersion}.");
                    });
                }
                ctrlElement.Select(e =>
                {
                    var value = e.Attribute("name").Value;
                    var lastDot = value.LastIndexOf('.');
                    var lastName = value.LastIndexOf("Controller");
                    var ctrlName = value.Substring(lastDot + 1, lastName - lastDot - 1);
                    return new
                    {
                        ctrlName,
                        summary = e.Element("summary").Value.Trim()
                    };
                }).ToList().ForEach(s => controllerDescDict[s.ctrlName] = s.summary);
            }
            return controllerDescDict;
        }
    }

    public class MySwaggerGenerator : ISwaggerProvider
    {

        private readonly IApiExplorer _apiExplorer;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly IDictionary<string, Info> _apiVersions;
        private readonly SwaggerGeneratorOptions _options;

        public MySwaggerGenerator(
            IApiExplorer apiExplorer,
            JsonSerializerSettings jsonSerializerSettings,
            IDictionary<string, Info> apiVersions,
            SwaggerGeneratorOptions options = null)
        {
            _apiExplorer = apiExplorer;
            _jsonSerializerSettings = jsonSerializerSettings;
            _apiVersions = apiVersions;
            _options = options ?? new SwaggerGeneratorOptions();
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var schemaRegistry = new SchemaRegistry(
                _jsonSerializerSettings,
                _options.CustomSchemaMappings,
                _options.SchemaFilters,
                _options.ModelFilters,
                _options.IgnoreObsoleteProperties,
                _options.SchemaIdSelector,
                _options.DescribeAllEnumsAsStrings,
                _options.DescribeStringEnumsInCamelCase,
                _options.ApplyFiltersToAllSchemas);

            Info info;
            _apiVersions.TryGetValue(apiVersion, out info);
            if (info == null)
                throw new UnknownApiVersion(apiVersion);
            float.TryParse(Regex.Match(apiVersion, @"^v(\d+)$").Groups[1].Value, out var v);
            var paths = GetApiDescriptionsFor(apiVersion)
                .Where(apiDesc => !(_options.IgnoreObsoleteActions && apiDesc.IsObsolete()))
                .OrderBy(_options.GroupingKeySelector, _options.GroupingKeyComparer)
                .GroupBy(apiDesc => apiDesc.RelativePathSansQueryString())
                .ToDictionary(group => "/" + group.Key, group => CreatePathItem(group, schemaRegistry, v));

            var rootUri = new Uri(rootUrl);
            var port = (!rootUri.IsDefaultPort) ? ":" + rootUri.Port : string.Empty;

            var swaggerDoc = new SwaggerDocument
            {
                info = info,
                host = rootUri.Host + port,
                basePath = (rootUri.AbsolutePath != "/") ? rootUri.AbsolutePath : null,
                schemes = (_options.Schemes != null) ? _options.Schemes.ToList() : new[] { rootUri.Scheme }.ToList(),
                paths = paths,
                definitions = schemaRegistry.Definitions,
                securityDefinitions = _options.SecurityDefinitions
            };

            foreach (var filter in _options.DocumentFilters)
            {
                filter.Apply(swaggerDoc, schemaRegistry, _apiExplorer);
            }

            return swaggerDoc;
        }

        private readonly object lockObj = new object();
        private readonly static ConcurrentBag<ApiDescription> okApiDesc = new ConcurrentBag<ApiDescription>();
        private IEnumerable<ApiDescription> GetApiDescriptionsFor(string apiVersion)
        {
            if (okApiDesc.Count == 0)
            {
                lock (lockObj)
                {
                    if (okApiDesc.Count == 0)
                    {
                        Regex regex = new Regex(@"(v\d+)/(\w+)(v\d+)/", RegexOptions.IgnoreCase);//匹配带有版本号的
                        Regex ctrl = new Regex(@"api/(\w+)(v\d+)/", RegexOptions.IgnoreCase);//匹配没有版本号的
                        Regex version = new Regex(@"\.(v\d+)", RegexOptions.IgnoreCase);
                        // 处理成有效的 ApiDescription
                        _apiExplorer.ApiDescriptions.Where(a =>
                        {
                            var path = a.RelativePath;
                            var cDescriptor = a.ActionDescriptor.ControllerDescriptor;
                            var @namespace = cDescriptor.ControllerType.Namespace;
                            Match match = regex.Match(path);
                            if (match.Success)//匹配成功，path里有版本号
                            {
                                var group1 = match.Groups[1].Value;//跟在/api 后面的版本号
                                var group2 = match.Groups[2].Value;//控制器名（大写） 没有匹配上则为空字符串
                                var group3 = match.Groups[3].Value;//版本号（大写） 没有匹配上则为空字符串
                                if (!group1.Equals(group3, StringComparison.OrdinalIgnoreCase))
                                {
                                    return false;
                                }
                                if (@namespace.EndsWith("." + group3, StringComparison.OrdinalIgnoreCase))
                                {
                                    a.RelativePath = a.RelativePath.Replace($"/{group2 + group3}/", $"/{group2[0] + group2.Substring(1).ToLower()}/");
                                    return true;
                                }
                                else if (group3 == "V1" && version.IsMatch(@namespace) == false)
                                {
                                    //命名空间没有版本号，如果path中版本号为v1也合理，默认v1
                                    a.RelativePath = a.RelativePath.Replace($"/{group2 + group3}/", $"/{group2[0] + group2.Substring(1).ToLower()}/");
                                    return true;
                                }
                                return false;
                            }
                            else
                            {
                                var ctrlMatch = ctrl.Match(path);
                                if (ctrlMatch.Success)
                                {
                                    if (version.IsMatch(@namespace) == false)
                                    {
                                        var cName = ctrlMatch.Groups[1].Value;//控制器名（大写） 
                                        var v = ctrlMatch.Groups[2].Value;//版本号（大写） 
                                        a.RelativePath = a.RelativePath.Replace($"/{cName + v}/", $"/{cName[0] + cName.Substring(1).ToLower()}/");
                                        return true;
                                    }

                                }

                            }
                            return false;
                        })
                        .ToList()
                        .ForEach(a => okApiDesc.Add(a));
                    }
                }
            }
            //根据版本号筛选 ApiDescription
            return okApiDesc.Where(apiDesc => _options.VersionSupportResolver(apiDesc, apiVersion)).ToList();
        }

        private PathItem CreatePathItem(IEnumerable<ApiDescription> apiDescriptions, SchemaRegistry schemaRegistry, float apiVersion)
        {
            var pathItem = new PathItem();

            // Group further by http method
            var perMethodGrouping = apiDescriptions
                .GroupBy(apiDesc => apiDesc.HttpMethod.Method.ToLower());

            foreach (var group in perMethodGrouping)
            {
                var httpMethod = group.Key;

                var apiDescription = (group.Count() == 1)
                    ? group.First()
                    : _options.ConflictingActionsResolver(group);

                switch (httpMethod)
                {
                    case "get":
                        pathItem.get = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "put":
                        pathItem.put = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "post":
                        pathItem.post = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "delete":
                        pathItem.delete = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "options":
                        pathItem.options = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "head":
                        pathItem.head = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                    case "patch":
                        pathItem.patch = CreateOperation(apiDescription, schemaRegistry, apiVersion);
                        break;
                }
            }

            return pathItem;
        }

        private Operation CreateOperation(ApiDescription apiDesc, SchemaRegistry schemaRegistry, float apiVersion)
        {
            var parameters = apiDesc.ParameterDescriptions
                .Select(paramDesc =>
                {
                    string location = GetParameterLocation(apiDesc, paramDesc);
                    return CreateParameter(location, paramDesc, schemaRegistry);
                })
                 .ToList();

            var responses = new Dictionary<string, Response>();
            var responseType = apiDesc.ResponseType();
            if (responseType == null || responseType == typeof(void))
                responses.Add("204", new Response { description = "No Content" });
            else
                responses.Add("200", new Response { description = "OK", schema = schemaRegistry.GetOrRegister(responseType) });

            var operation = new Operation
            {
                tags = new[] { _options.GroupingKeySelector(apiDesc) },
                operationId = apiDesc.FriendlyId(),
                produces = apiDesc.Produces().ToList(),
                consumes = apiDesc.Consumes().ToList(),
                parameters = parameters.Any() ? parameters : new List<Parameter>(), // parameters can be null but not empty
                responses = responses,
                deprecated = apiDesc.IsObsolete() ? true : (bool?)null
            };

            if (apiVersion > 2)
            {
                operation.parameters.Insert(0, new Parameter { name = "JWT", @in = "header", description = "Json Web Token", required = false, type = "string" });
            }
            else
            {
                //appInfo控制器排除
                if (apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName != nameof(Controllers.v1.AppInfoController))
                {
                    operation.parameters.Insert(0, new Parameter { name = "AppKey", @in = "header", description = "客户端标识", required = false, type = "string" });
                    operation.parameters.Insert(1, new Parameter { name = "Sign", @in = "header", description = "签名", required = false, type = "string" });
                }
            }


            foreach (var filter in _options.OperationFilters)
            {
                filter.Apply(operation, schemaRegistry, apiDesc);
            }

            return operation;
        }

        private string GetParameterLocation(ApiDescription apiDesc, ApiParameterDescription paramDesc)
        {
            if (apiDesc.RelativePathSansQueryString().Contains("{" + paramDesc.Name + "}"))
                return "path";
            else if (paramDesc.Source == ApiParameterSource.FromBody)
                return "body";
            else
                return "query";
        }

        private Parameter CreateParameter(string location, ApiParameterDescription paramDesc, SchemaRegistry schemaRegistry)
        {
            var parameter = new Parameter
            {
                @in = location,
                name = paramDesc.Name
            };

            if (paramDesc.ParameterDescriptor == null)
            {
                parameter.type = "string";
                parameter.required = true;
                return parameter;
            }

            parameter.required = location == "path" || !paramDesc.ParameterDescriptor.IsOptional;
            parameter.@default = paramDesc.ParameterDescriptor.DefaultValue;

            var schema = schemaRegistry.GetOrRegister(paramDesc.ParameterDescriptor.ParameterType);
            if (parameter.@in == "body")
                parameter.schema = schema;
            else
                parameter.PopulateFrom(schema);

            return parameter;
        }

    }
}
