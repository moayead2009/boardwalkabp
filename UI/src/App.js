import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";

// Auth routes
import UserLogin from "./components/Builder/Users/UserLogin";
import UserRegister from "./components/Builder/Users/UserRegister";
import ClientRegister from "./components/Builder/Clients/ClientRegister";
import ClientLogin from "./components/Builder/Clients/ClientLogin";

// Builder routes
import BuilderHome from "./components/Builder/Home";
import BuilderLayout from "./components/Builder/layout/BuilderLayout";
import Applications from "./components/Builder/Applications/Applications";
import AddApplication from "./components/Builder/Applications/AddApplication";
import ViewApplication from "./components/Builder/Applications/ViewApplication";
import EditApplication from "./components/Builder/Applications/EditApplication";
import DeleteApplication from "./components/Builder/Applications/DeleteApplication";
import Categories from "./components/Builder/Categories/Categories";
import AddCategory from "./components/Builder/Categories/AddCategory";
import EditCategory from "./components/Builder/Categories/EditCategory";
import ViewCategory from "./components/Builder/Categories/ViewCategory";
import DeleteCategory from "./components/Builder/Categories/DeleteCategory";
import Clients from "./components/Builder/Clients/Clients";
import EditClient from "./components/Builder/Clients/EditClient";
import ViewClient from "./components/Builder/Clients/ViewClient";
import DeleteClient from "./components/Builder/Clients/DeleteClient";
import Questions from "./components/Builder/Questions/Questions";
import ViewQuestion from "./components/Builder/Questions/ViewQuestion";
import AddQuestion from "./components/Builder/Questions/AddQuestion";
import EditQuestion from "./components/Builder/Questions/EditQuestion";
import DeleteQuestion from "./components/Builder/Questions/DeleteQuestion";

// Viewer routes
import ViewerHome from "./components/Viewer/Home";
import ViewerLayout from "./components/Viewer/layout/ViewerLayout";
import ClientApplications from "./components/Viewer/ClientApplications";
import Profile from "./components/Viewer/Profile";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/register" exact element={<UserRegister />} />
        <Route path="/" exact element={<UserLogin />} />
        <Route path="/clientRegister" exact element={<ClientRegister />} />
        <Route path="/clientLogin" exact element={<ClientLogin />} />
        <Route path="*" exact element={<h1>404 Not Found</h1>} />

        {/* Builder routes */}
        <Route path="/builder" exact element={<BuilderLayout />}>
          <Route path="/builder/home" exact element={<BuilderHome />} />

          <Route
            path="/builder/applications"
            exact
            element={<Applications />}
          />
          <Route
            path="/builder/applications/add"
            exact
            element={<AddApplication />}
          />
          <Route
            path="/builder/applications/view/:id"
            exact
            element={<ViewApplication />}
          />
          <Route
            path="/builder/applications/edit/:id"
            exact
            element={<EditApplication />}
          />
          <Route
            path="/builder/applications/delete/:id"
            exact
            element={<DeleteApplication />}
          />

          <Route path="/builder/categories/" exact element={<Categories />} />
          <Route
            path="/builder/categories/add"
            exact
            element={<AddCategory />}
          />
          <Route
            path="/builder/categories/edit/:id"
            exact
            element={<EditCategory />}
          />
          <Route
            path="/builder/categories/view/:id"
            exact
            element={<ViewCategory />}
          />
          <Route
            path="/builder/categories/delete/:id"
            exact
            element={<DeleteCategory />}
          />

          <Route path="/builder/clients" exact element={<Clients />} />
          <Route
            path="/builder/clients/add"
            exact
            element={<ClientRegister />}
          />
          <Route
            path="/builder/clients/edit/:id"
            exact
            element={<EditClient />}
          />
          <Route
            path="/builder/clients/view/:id"
            exact
            element={<ViewClient />}
          />
          <Route
            path="/builder/clients/delete/:id"
            exact
            element={<DeleteClient />}
          />

          <Route path="/builder/questions" exact element={<Questions />} />
          <Route
            path="/builder/questions/add"
            exact
            element={<AddQuestion />}
          />
          <Route
            path="/builder/questions/edit/:id"
            exact
            element={<EditQuestion />}
          />
          <Route
            path="/builder/questions/view/:id"
            exact
            element={<ViewQuestion />}
          />
          <Route
            path="/builder/questions/delete/:id"
            exact
            element={<DeleteQuestion />}
          />
        </Route>

        {/* Viewer routes */}
        <Route path="/" exact element={<ViewerLayout />}>
          <Route path="/viewer/home" exact element={<ViewerHome />} />
          <Route
            path="/viewer/applications/:id"
            exact
            element={<ClientApplications />}
          />
          <Route path="/viewer/profile" exact element={<Profile />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
