import {
  Entity,
  Column,
  PrimaryColumn,
  BeforeInsert,
  BeforeUpdate,
} from 'typeorm';

@Entity('Geral')
export class Geral {
  @PrimaryColumn({ type: 'int' })
  rcacode: number;

  @PrimaryColumn({ type: 'varchar', length: 255 })
  nome: string;

  @Column({ type: 'int' })
  codsupervisor: number;

  @Column({ type: 'varchar', length: 255 })
  manager: string;

  @Column({ type: 'int' })
  point: number;

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  faturamento: number;

  @Column({ type: 'int', default: 0 })
  cupons: number;

  @Column({ type: 'int', default: 0 })
  pontos: number;

  @Column({ type: 'varchar', length: 255 }) 
  equipe: string;

  @BeforeInsert()
  @BeforeUpdate()
  calcularCupons() {
    this.cupons = Math.floor(this.pontos / 500000);
  }
}
