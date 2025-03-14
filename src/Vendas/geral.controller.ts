import { Controller, Get } from "@nestjs/common";
import { GeralService } from "./geral.service";
import { Geral } from "./entities/geral.entity";


@Controller('Geral')
export class GeralController {
    constructor(private readonly GeralService: GeralService) {}

    @Get()
    async getAllResumoVendas(): Promise<Geral[]> {
        return this.GeralService.findAll();
    }
}