using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// 取得所有用戶
        /// </summary>
        /// <returns>用戶列表</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得用戶列表時發生錯誤");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        /// <summary>
        /// 根據ID取得特定用戶
        /// </summary>
        /// <param name="id">用戶ID</param>
        /// <returns>用戶資訊</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"找不到ID為 {id} 的用戶");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得用戶 {UserId} 時發生錯誤", id);
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        /// <summary>
        /// 建立新用戶
        /// </summary>
        /// <param name="user">用戶資訊</param>
        /// <returns>新建立的用戶</returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdUser = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "建立用戶時發生錯誤");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        /// <summary>
        /// 更新用戶資訊
        /// </summary>
        /// <param name="id">用戶ID</param>
        /// <param name="user">更新的用戶資訊</param>
        /// <returns>更新後的用戶</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedUser = await _userService.UpdateUserAsync(id, user);
                if (updatedUser == null)
                {
                    return NotFound($"找不到ID為 {id} 的用戶");
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新用戶 {UserId} 時發生錯誤", id);
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        /// <param name="id">用戶ID</param>
        /// <returns>刪除結果</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound($"找不到ID為 {id} 的用戶");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "刪除用戶 {UserId} 時發生錯誤", id);
                return StatusCode(500, "內部伺服器錯誤");
            }
        }
    }
} 