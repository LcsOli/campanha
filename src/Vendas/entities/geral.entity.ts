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

  @Column({ type: 'decimal', precision: 10, scale: 2 })
  faturamento: number;

  // pontos vindos de vendas/atividades
  @Column({ type: 'int', default: 0 })
  pontos: number;

  // quantos clientes foram positivados (cada um dá +50.000 pontos, por exemplo)
  @Column({ type: 'int', default: 0 })
  clientes_positivados: number;

  // cupons = floor( (pontos + clientes_positivados*50k) / 500k )
  @Column({ type: 'int', default: 0 })
  cupons: number;

  @Column({ type: 'varchar', length: 255 })
  equipe: string;

  @Column({ type: 'int', default: 0 })
  meta: number;

  @BeforeInsert()
  @BeforeUpdate()
  calcularCupons() {
    const totalPontos =
      (this.pontos ?? 0) + (this.clientes_positivados ?? 0) * 50000;
    this.cupons = Math.floor(totalPontos / 500000);
  }
}
