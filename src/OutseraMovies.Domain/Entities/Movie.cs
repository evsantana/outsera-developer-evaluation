using OutseraMovies.Domain.Validation;

namespace OutseraMovies.Domain.Entities
{
    public sealed class Movie : BaseEntity
    {

        #region Constructors
        public Movie()
        { }
        public Movie(int year, string title, string studios, bool winner, string producers)
        {
            ValidateDomain(year, title, studios, producers);

            Winner = winner;
        }
        public Movie(int id, int year, string title, string studios, bool winner, string producers)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;

            ValidateDomain(year, title, studios, producers);

            Winner = winner;
        }
        #endregion

        #region Properties
        public int Year { get; private set; }
        public string Title { get; private set; }
        public string Studios { get; private set; }
        public bool Winner { get; private set; }
        public string Producers { get; private set; }
        #endregion

        #region Methods
        private void ValidateDomain(int year, string title, string studios, string producers)
        {
            DomainExceptionValidation.When(year.ToString().Length < 4, "Invalid Year. Year format must be: XXXX");
            DomainExceptionValidation.When(string.IsNullOrEmpty(title), "Invalid Title. Title is required");
            DomainExceptionValidation.When(string.IsNullOrEmpty(studios), "Invalid Studios. Studios is required");
            DomainExceptionValidation.When(string.IsNullOrEmpty(producers), "Invalid Producers. Producers is required");

            Year = year;
            Title = title;
            Studios = studios;
            Producers = producers;
        }

        public void Update(int year, string title, string studios, bool winner, string producers)
        {
            ValidateDomain(year, title, studios, producers);
            Winner = winner;
        }
        #endregion
    }
}
