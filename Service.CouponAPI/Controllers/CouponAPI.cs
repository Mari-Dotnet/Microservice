using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.CouponAPI.Data;
using Service.CouponAPI.Models;
using Service.CouponAPI.Models.Dto;

namespace Service.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPI : ControllerBase
    {
        private readonly AppDbContext _dbcontenxt;
        private readonly ResponseDto _responseDto;
        private IMapper _mapper;
        public CouponAPI(AppDbContext dbcontext, IMapper mapper)
        {
            _dbcontenxt = dbcontext;
            _responseDto=new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetallCouopn()
        {
            try
            {
                _responseDto.IsSucessfull = true;
                _responseDto.Result=_mapper.Map<List<CouponDto>>(await _dbcontenxt.Coupons.ToListAsync());
            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull=false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;


        }

        [HttpGet]
        [Route("id")]
        public async Task<ActionResult<ResponseDto>> GetCouopn(int id)
        {
            try
            {
                _responseDto.Result= _mapper.Map<CouponDto>(await _dbcontenxt.Coupons.FirstOrDefaultAsync(x => x.CouponId== id));
                _responseDto.IsSucessfull = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull = false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("code")]
        public async Task<ActionResult<ResponseDto>> Getbycode(string code)
        {
            try
            {
                _responseDto.Result = _mapper.Map<CouponDto>(await _dbcontenxt.Coupons.FirstOrDefaultAsync(x => x.CouponCode.ToLower().Equals(code.ToLower())));
                _responseDto.IsSucessfull = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull = false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> SaveCoupon([FromBody] CouponDto model)
        {
            try
            {
                if (model is not null)
                {
                    Coupon couopn = _mapper.Map<Coupon>(model);
                    await _dbcontenxt.Coupons.AddAsync(couopn);
                    _dbcontenxt.SaveChanges();
                    _responseDto.Result = _mapper.Map<CouponDto>(couopn); ;
                    _responseDto.IsSucessfull = true;
                }
                else
                {
                    _responseDto.Result = model;
                    _responseDto.IsSucessfull = true;
                }
                
            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull = false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;
        }
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateCoupon([FromBody] CouponDto model)
        {
            try
            {
                if (model is not null)
                {
                    Coupon couopn = _mapper.Map<Coupon>(model);
                     _dbcontenxt.Coupons.Update(couopn);
                    _dbcontenxt.SaveChanges();
                    _responseDto.Result = _mapper.Map<CouponDto>(couopn); ;
                    _responseDto.IsSucessfull = true;
                }
                else
                {
                    _responseDto.Result = model;
                    _responseDto.IsSucessfull = true;
                }

            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull = false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(int id)
        {
            try
            {
                if (id > 0)
                {
                    Coupon data = await _dbcontenxt.Coupons.FirstOrDefaultAsync(x => x.CouponId == id);
                    if(data is not null)
                    {
                        _dbcontenxt.Coupons.Remove(data);
                        _dbcontenxt.SaveChanges();
                        _responseDto.Result = _mapper.Map<CouponDto>(data); ;
                        _responseDto.IsSucessfull = true;
                    }
                }
                else
                {
                    _responseDto.Result = id;
                    _responseDto.IsSucessfull = true;
                }

            }
            catch (Exception ex)
            {
                _responseDto.IsSucessfull = false;
                _responseDto.Message = ex.StackTrace;
                throw;
            }
            return _responseDto;
        }


    }
}
