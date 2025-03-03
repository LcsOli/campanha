import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn } from 'typeorm';

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

  @CreateDateColumn({ type: 'timestamp', default: () => "CURRENT_TIMESTAMP", nullable: true })
  acesso: Date; 
}
