import { IsString, IsInt, IsDateString } from 'class-validator';

export class CreateUsuarioDto {
  @IsString()
  nome: string;

  @IsString()
  password: string;

  @IsInt()
  grupo: number;

  @IsDateString()
  acesso: string;

  @IsString()
  CPF: string;
}
