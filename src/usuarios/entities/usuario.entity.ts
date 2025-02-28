import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('usuario') 
export class Usuario {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ type: 'varchar', length: 11, unique: true })
  cpf: string; 

  @Column({ type: 'varchar', length: 255 })
  nome: string;

  @Column({ type: 'varchar', length: 255 })
  senha: string; 

  @Column({ type: 'int' })
  grupo: number;

  @Column({ type: 'date' })
  acesso: string;
}
