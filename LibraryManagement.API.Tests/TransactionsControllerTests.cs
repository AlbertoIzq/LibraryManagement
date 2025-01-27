using LibraryManagement.API.Controllers;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using LibraryManagement.Business.Models.DTO;
using Moq;

namespace LibraryManagement.API.Tests
{
    public class TransactionsControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITransactionRepository> _mockTransactionRepo;
        private readonly Mock<IBookRepository> _mockBookRepo;
        private readonly Mock<IMemberRepository> _mockMemberRepo;

        private readonly TransactionsController _transactionsController;

        public TransactionsControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTransactionRepo = new Mock<ITransactionRepository>();
            _mockBookRepo = new Mock<IBookRepository>();
            _mockMemberRepo = new Mock<IMemberRepository>();

            _mockUnitOfWork.Setup(uow => uow.Transactions).Returns(_mockTransactionRepo.Object);
            _mockUnitOfWork.Setup(uow => uow.Books).Returns(_mockBookRepo.Object);
            _mockUnitOfWork.Setup(uow => uow.Members).Returns(_mockMemberRepo.Object);

            _transactionsController = new TransactionsController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task BorrowBookAsync_ShouldThrow_WhenBorrowLimitReached()
        {
            // Data
            var borrowBookDto = new BorrowBookDto()
            {
                MemberId = 1,
                BookId = 6
            };

            // Arrange
            _mockTransactionRepo
                .Setup(repo => repo.BorrowBookAsync(borrowBookDto.MemberId, borrowBookDto.BookId))
                .ThrowsAsync(new InvalidOperationException("Member cannot borrow more books because the limit has been reached"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionsController.Borrow(borrowBookDto));
            Assert.Equal("Member cannot borrow more books because the limit has been reached", exception.Message);
        }
    }
}
