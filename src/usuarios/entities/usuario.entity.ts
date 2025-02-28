import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Usuario {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  nome: string;

  @Column()
  senha: string;  

  @Column()
  grupo: number;

  @Column({ type: 'date' })
  acesso: string;

  @Column()
  CPF: string;
}
