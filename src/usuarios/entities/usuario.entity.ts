import { Entity, PrimaryGeneratedColumn, Column, UpdateDateColumn } from 'typeorm';

@Entity('usuario') 
export class Usuario {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ type: 'bigint', unique: true })
  cpf: string;

  @Column({ type: 'varchar', length: 255 })
  nome: string; 

  @Column({ type: 'varchar', length: 255, nullable: false })
  senha: string;

  @Column({ type: 'int' })
  grupo: number;

  @Column({ type: 'timestamp', nullable: true })
  acesso: Date; 

  @Column({ type: 'varchar', length: 255 })
  participacao: string; 

}
