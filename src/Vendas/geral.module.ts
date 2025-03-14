import { Module } from "@nestjs/common";
import { TypeOrmModule } from "@nestjs/typeorm";
import { GeralService } from "./geral.service";
import { GeralController } from "./geral.controller";
import { Geral } from "./entities/geral.entity";


@Module({
    imports: [TypeOrmModule.forFeature([Geral])],
    controllers: [GeralController],
    providers: [GeralService],
})
export class GeralModule {}