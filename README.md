# WatchBox

#### Video Demo: [WatchBox Demo](https://www.youtube.com/watch?v=moqJXLgmtzI&ab_channel=MauroDev)

#### Description
I built my app to keep track of the movies I've watched, as I often forget them. It helps users discover new movies and allows sharing with friends and family, making it easier to recommend and discuss films.

#### Features
- **Search Functionality**: Users can enter a movie title to fetch details and recommendations.
- **Random Movie Recommendations**: The app provides random movie recommendations every time the user logs in.
- **Sharing Options**: Users can share their favorite movies with friends, facilitating discussions and recommendations.
- **Error Handling**: The application handles errors related to internet connectivity and unexpected data formats.
- **User-Friendly Interface**: The design is aimed at providing a seamless experience with intuitive navigation.

#### Project Structure
The project consists of several C# classes and components that work together to deliver the app's functionality, using a multilayered software architecture. Below is a brief description of each key file:

#### Search.cs
This class is responsible for fetching movie data from an external API I built to hide the API key from OMDB, which is responsible for fetching the movie data. It contains the `fetchMovie` method, which constructs the API request based on user input and handles potential errors that may arise during the request.

#### FormMovies.cs, FormShows.cs, FormFavorites.cs, FormSearched.cs, FormShare.cs
These forms provide additional functionalities like viewing movies, TV shows, favorite movies, detailed search results, and sharing options. Each form is designed to enhance the user experience by offering dedicated spaces for specific actions. Each form inherits from `Form` and implements its specific layout and functionalities, allowing for modularity and ease of maintenance and flexibility.

#### Design Choices
Several design choices were made throughout the development of WatchBox to ensure optimal functionality and user experience:

- **Separation of Concerns**: Each class in the application is designed with a single responsibility. For example, the `Search` class only deals with fetching movie data, while UI-related functionalities are handled by form classes. This modularity facilitates easier maintenance and testing.

- **Async Programming**: The use of asynchronous programming (`async` and `await`) in fetching movie data prevents the UI from freezing during network calls, improving the overall user experience.
