using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services
{
    public interface IBaseService<TResultDto, TCreateDto, TUpdateDto>
    {
        Task<UIApiResponse<IReadOnlyCollection<TResultDto>>> GetListAsync(Guid? id = null);
        Task<UIApiResponse<TResultDto>> GetAsync(Guid id);
        Task<UIApiResponse<bool>> CreateAsync(TCreateDto createDto);
        Task<UIApiResponse<bool>> UpdateAsync(TUpdateDto updateDto);
        Task<UIApiResponse<bool>> DeleteAsync(Guid id);
    }
}