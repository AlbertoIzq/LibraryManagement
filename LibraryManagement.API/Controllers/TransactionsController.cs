﻿using AutoMapper;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using LibraryManagement.Business.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("Borrow")]
        [HttpPost]
        public async Task<IActionResult> Borrow([FromBody] BorrowBookDto borrowBookDto)
        {
            // Use Domain Model to create Member
            var transaction = await _unitOfWork.Transactions.BorrowBookAsync(borrowBookDto.MemberId,
                borrowBookDto.BookId);
            await _unitOfWork.SaveAsync();

            // Show information to the client
            return Ok(transaction);
        }

        [Route("Return")]
        [HttpPost]
        public async Task<IActionResult> Return([FromBody] ReturnBookDto returnBookDto)
        {
            // Use Domain Model to create Member
            var transaction = await _unitOfWork.Transactions.ReturnBookAsync(returnBookDto.MemberId,
                returnBookDto.BookId);
            await _unitOfWork.SaveAsync();

            // Show information to the client
            return Ok(transaction);
        }

        [Route("Overdue")]
        [HttpGet]
        public async Task<IActionResult> GetOverdueBooks()
        {
            // Use Domain Model to create Member
            var books = await _unitOfWork.Transactions.GetOverdueBooksAsync();

            // Show information to the client
            return Ok(books);
        }
    }
}