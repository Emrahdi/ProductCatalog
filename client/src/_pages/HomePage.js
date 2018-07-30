import * as React from 'react';
import { connect } from 'react-redux';
class HomePage extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { user, users } = this.props;
        return (React.createElement("div", null,
            React.createElement("h1", null, "Hello, world!"),
            React.createElement("p", null, "Welcome to your new Product Catalog Application, built with:"),
            React.createElement("ul", null,
                React.createElement("li", null,
                    React.createElement("a", { href: 'https://get.asp.net/' }, "ASP.NET Core"),
                    " and ",
                    React.createElement("a", { href: 'https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx' }, "C#"),
                    " for cross-platform server-side code"),
                React.createElement("li", null,
                    React.createElement("a", { href: 'https://facebook.github.io/react/' }, "React"),
                    " and ",
                    React.createElement("a", { href: 'http://www.typescriptlang.org/' }, "TypeScript"),
                    " for client-side code"),
                React.createElement("li", null,
                    React.createElement("a", { href: 'https://webpack.github.io/' }, "Webpack"),
                    " for building and bundling client-side resources"),
                React.createElement("li", null,
                    React.createElement("a", { href: 'https://ant.design/docs/react/introduce' }, "Ant Design"),
                    " for layout and styling")),
            React.createElement("p", null, "To help you get started, we've also set up:"),
            React.createElement("ul", null,
                React.createElement("li", null,
                    React.createElement("strong", null, "Client-side navigation"),
                    ". For example, click ",
                    React.createElement("em", null, "Counter"),
                    " then ",
                    React.createElement("em", null, "Back"),
                    " to return here."),
                React.createElement("li", null,
                    React.createElement("strong", null, "Webpack dev middleware"),
                    ". In development mode, there's no need to run the ",
                    React.createElement("code", null, "webpack"),
                    " build tool. Your client-side resources are dynamically built on demand. Updates are available as soon as you modify any file."),
                React.createElement("li", null,
                    React.createElement("strong", null, "Hot module replacement"),
                    ". In development mode, you don't even need to reload the page after making most changes. Within seconds of saving changes to files, rebuilt React components will be injected directly into your running application, preserving its live state."),
                React.createElement("li", null,
                    React.createElement("strong", null, "Efficient production builds"),
                    ". In production mode, development-time features are disabled, and the ",
                    React.createElement("code", null, "webpack"),
                    " build tool produces minified static CSS and JavaScript files.")),
            React.createElement("h4", null, "Going further"),
            React.createElement("p", null,
                "For larger applications, or for server-side prerendering (i.e., for ",
                React.createElement("em", null, "isomorphic"),
                " or ",
                React.createElement("em", null, "universal"),
                " applications), you should consider using a Flux/Redux-like architecture. You can generate an ASP.NET Core application with React and Redux using ",
                React.createElement("code", null, "dotnet new reactredux"),
                " instead of using this template.")));
    }
}
function mapStateToProps(state) {
    const { users, authentication } = state;
    const { user } = authentication;
    return {
        user,
        users
    };
}
const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };
//# sourceMappingURL=HomePage.js.map