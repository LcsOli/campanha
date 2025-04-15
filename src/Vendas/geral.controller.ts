import { Controller, Post, Body } from '@nestjs/common';
import { GeralService } from './geral.service';

@Controller('geral')
export class GeralController {
  constructor(private readonly geralService: GeralService) {}

  @Post('acesso')
  registrarAcesso(@Body('cpf') cpf: string) {
    return this.geralService.registrarAcesso(cpf);
  }
}
