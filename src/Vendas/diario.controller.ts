import { Controller, Get } from '@nestjs/common';
import { Diario } from './entities/diario.entity';
import { DiarioService } from './diario.service';


@Controller('diario')
export class DiarioController {
  constructor(private readonly DiarioService: DiarioService) {}

  @Get()
  async getAllResumoVendas(): Promise<Diario[]> {
    return this.DiarioService.findAll();
  }
}
