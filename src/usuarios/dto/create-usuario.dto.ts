import { IsString, IsNotEmpty, IsNumber, IsDateString } from 'class-validator';

export class CreateUsuarioDto {
  @IsNotEmpty()
  @IsString()
  nome: string;

  @IsNotEmpty()
  @IsString()
  senha: string;

  @IsNotEmpty()
  @IsNumber()
  grupo: number; 

  @IsNotEmpty()
  @IsDateString()
  acesso: string; 

  @IsNotEmpty()
  @IsString()
  CPF: string; 
}
