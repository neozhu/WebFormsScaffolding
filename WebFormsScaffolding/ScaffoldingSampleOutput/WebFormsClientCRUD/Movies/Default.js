/// <reference path="~/Scripts/jquery-2.0.3.min.js" />
/// <reference path="~/Scripts/knockout-3.0.0.js" />
/// <reference path="Scaffolding.WebForms.js" />

function Movie(movie) {
    this.Id = movie ? ko.observable(movie.Id) : ko.observable();
    this.Title = movie?ko.observable(movie.Title):ko.observable();
    this.Director = movie?ko.observable(movie.Director):ko.observable();
    this.TicketPrice = movie?ko.observable(movie.TicketPrice):ko.observable();
}

function DefaultViewModel() {
    var self = this;

    this.selectedMovie = ko.observable();
    this.movieToUpdate = ko.observable();
    this.movieToCreate = ko.observable();


    this.loading = ko.observable(false);

    this.movieModal = ko.observable(false);

    this.showPane = function (pane) {
        self.detailsPane(pane == 'details');
        self.createPane(pane == 'create');
        self.updatePane(pane == 'update');
        self.deletePane(pane == 'delete');
    },

    this.hideMovieModal = function () {
        self.movieModal(false);
    };

    /* Details */
    this.detailsPane = ko.observable(true);

    this.showMovieDetails = function (item) {
        self.selectedMovie(item);
        self.movieModal(true);
        self.showPane('details');
    };



    /* List */
    this.movies = ko.observableArray();
    
    this.getMovies = function () {
        self.loading(true);
        Scaffolding.WebForms.invokeAction('/Api/Movies').done(function (movies) {
            $.each(movies, function (index, movie) {
                self.movies.push(new Movie(movie));
                self.loading(false);
            });
        });
    },



    /* Update */
    this.updatePane = ko.observable(false);

    this.movieToUpdateValidationErrors = ko.observableArray();

    this.showMovieUpdate = function() {
        self.movieToUpdate(ko.toJS(self.selectedMovie));
        self.movieToUpdateValidationErrors([]);
        self.showPane('update');
    };


    this.updateMovie = function () {
        // Make the Ajax call
        var data = ko.toJSON(this.movieToUpdate);
        Scaffolding.WebForms.invokeAction('/Api/Movies', 'PUT', data).done(function (movieUpdate) {
            // success!
            self.selectedMovie().Title(movieUpdate.Title);
            self.selectedMovie().Director(movieUpdate.Director);
            self.selectedMovie().TicketPrice(movieUpdate.TicketPrice);

            self.showPane('details');
        }).fail(function (errors) {
            self.movieToUpdateValidationErrors(errors);
        });
    };

    /* Create */
    this.createPane = ko.observable(false);

    this.movieToCreateValidationErrors = ko.observableArray();

    this.showMovieCreate = function() {
        self.movieToCreate(new Movie());
        self.movieToCreateValidationErrors([]);

        self.movieModal(true);
        self.showPane('create');
    };

    this.createMovie = function () {
        var data = ko.toJSON(this.movieToCreate);
        
        Scaffolding.WebForms.invokeAction('/Api/Movies', 'POST', data).done(function(movieCreated) {
            // success!
            self.selectedMovie(new Movie());
            self.selectedMovie().Id(movieCreated.Id);
            self.selectedMovie().Title(movieCreated.Title);
            self.selectedMovie().Director(movieCreated.Director);
            self.selectedMovie().TicketPrice(movieCreated.TicketPrice);

            self.movies.push(self.selectedMovie());
            self.showPane('details');
        }).fail(function (errors) {
            self.movieToCreateValidationErrors(errors);
        });
    };

    /* Delete */
    this.deletePane = ko.observable(false);

    this.movieToDeleteValidationErrors = ko.observableArray();


    this.showMovieDelete = function(item) {
        self.selectedMovie(item);
        self.movieModal(true);
        self.showPane('delete');
    };

    this.deleteMovie = function () {
        var data = ko.toJSON(this.selectedMovie);

        Scaffolding.WebForms.invokeAction('/Api/Movies', 'DELETE', data).done(function (movieCreated) {
            self.movies.remove(self.selectedMovie());
            self.hideMovieModal();
        }).fail(function (errors) {
            self.movieToDeleteValidationErrors(errors);
        });

    };

};

var defaultViewModel = new DefaultViewModel();

