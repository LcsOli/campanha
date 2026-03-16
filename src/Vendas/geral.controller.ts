import { Controller, Post, Body, Get, Query, HttpCode, HttpStatus } from '@nestjs/common';
import { GeralService } from './geral.service';

@Controller('geral')
export class GeralController {
  constructor(private readonly geralService: GeralService) {}

  @Post('acesso')
  registrarAcesso(@Body('cpf') cpf: string) {
    return this.geralService.registrarAcesso(cpf);
  }

  // ✅ Nova rota GET /geral
  @Get()
  buscarGeral(@Query('equipe') equipe?: string) {
    return this.geralService.buscarGeral(equipe);
  }
  @Post('simular-pontos')
async simularPontos(@Body('nome') nome: string) {
  return this.geralService.simularPontuacaoCompleta(nome);
}
@Post('atualizar-positivados')
@HttpCode(HttpStatus.NO_CONTENT)
async atualizarPositivados(): Promise<void> {
  await this.geralService.atualizarPositivados();
}

}
