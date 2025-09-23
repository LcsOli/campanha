// import { Injectable } from '@nestjs/common';
// import { InjectRepository } from '@nestjs/typeorm';
// import { Repository } from 'typeorm';
// import { Geral } from './entities/geral.entity';
// import { Usuario } from 'src/usuarios/entities/usuario.entity';
// import * as moment from 'moment';

// @Injectable()
// export class GeralService {
//   constructor(
//     @InjectRepository(Geral)
//     private readonly geralRepository: Repository<Geral>,

//     @InjectRepository(Usuario)
//     private readonly usuarioRepository: Repository<Usuario>,
//   ) {}

//   async registrarAcesso(cpf: string): Promise<string> {
//     const usuario = await this.usuarioRepository.findOne({ where: { cpf } });
  
//     if (!usuario) {
//       return 'Usuário não encontrado';
//     }
  
//     const hoje = moment();
//     const ultimaPontuacao = usuario.ultimaPontuacaoSemanal
//       ? moment(usuario.ultimaPontuacaoSemanal)
//       : null;
  
//     const mesmaSemana =
//       ultimaPontuacao &&
//       hoje.isoWeek() === ultimaPontuacao.isoWeek() &&
//       hoje.year() === ultimaPontuacao.year();
  
//     if (mesmaSemana) {
//       return 'Usuário já recebeu ponto nesta semana';
//     }
  
//     const geral = await this.geralRepository.findOne({ where: { nome: usuario.nome } });
  
//     if (!geral) {
//       return 'Registro na tabela Geral não encontrado';
//     }
  
//     // 👉 Adiciona os pontos
//     geral.pontos = (geral.pontos || 0) + 50000;
  
//     // 🧠 Calcula os cupons baseado nos pontos (500.000 pontos = 1 cupom)
//     geral.cupons = Math.floor(geral.pontos / 500000);
  
//     await this.geralRepository.save(geral);
  
//     // Atualiza o usuário com nova data
//     usuario.ultimaPontuacaoSemanal = hoje.toDate();
//     await this.usuarioRepository.save(usuario);
  
//     return 'Ponto de acesso semanal computado com sucesso';
//   }

//   // ✅ Novo método para buscar os dados da tabela Geral
//   async buscarGeral(equipe?: string): Promise<any[]> {
//     const where: any = {};
//     if (equipe) where.equipe = equipe;
  
//     const dados = await this.geralRepository.find({
//       where,
//       order: { pontos: 'DESC' },
//     });
  
//     return dados.map((item) => {
//       const pontosNum = item.pontos ?? 0;
//       const cuponsCalc = Math.floor(pontosNum / 500_000);
  
//       return {
//         ...item,
//         pontos: pontosNum.toLocaleString('pt-BR'),
//         cupons: cuponsCalc.toLocaleString('pt-BR'),
//         faturamento: (item.faturamento ?? 0).toLocaleString('pt-BR', {
//           style: 'currency',
//           currency: 'BRL',
//           minimumFractionDigits: 2,
//         }),
//       };
//     });
//   }
//   async simularPontuacaoCompleta(nome: string): Promise<string> {
//     const geral = await this.geralRepository.findOne({ where: { nome } });
  
//     if (!geral) {
//       return 'Usuário não encontrado na tabela Geral.';
//     }
  
//     geral.pontos = 500000; // Atualiza os pontos para 500 mil
//     await this.geralRepository.save(geral); // Isso dispara o cálculo automático dos cupons
  
//     return `Pontuação atualizada para 500.000. Cupons agora: ${geral.cupons}`;
//   }
//   async atualizarPositivados(): Promise<void> {
//     await this.geralRepository.query(`
//       UPDATE geral
//       SET pontos = pontos + clientes_positivados * 50000,
//           cupons = FLOOR((pontos + clientes_positivados * 50000) / 500000)
//     `);
//   }
// }  
import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Geral } from './entities/geral.entity';
import { Usuario } from 'src/usuarios/entities/usuario.entity';
import * as moment from 'moment';

@Injectable()
export class GeralService {
  constructor(
    @InjectRepository(Geral)
    private readonly geralRepository: Repository<Geral>,

    @InjectRepository(Usuario)
    private readonly usuarioRepository: Repository<Usuario>,
  ) {}

  /**
   * Dá 50.000 pontos 1x por semana.
   * Ajusta cupons apenas pela DIFERENÇA de faixas de 500k,
   * preservando cupons lançados manualmente.
   */
  async registrarAcesso(cpf: string): Promise<string> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf } });
    if (!usuario) return 'Usuário não encontrado';

    const hoje = moment();
    const ultimaPontuacao = usuario.ultimaPontuacaoSemanal
      ? moment(usuario.ultimaPontuacaoSemanal)
      : null;

    const mesmaSemana =
      !!ultimaPontuacao &&
      hoje.isoWeek() === ultimaPontuacao.isoWeek() &&
      hoje.year() === ultimaPontuacao.year();

    if (mesmaSemana) {
      return 'Usuário já recebeu ponto nesta semana';
    }

    const geral = await this.geralRepository.findOne({ where: { nome: usuario.nome } });
    if (!geral) return 'Registro na tabela Geral não encontrado';

    const p0 = geral.pontos || 0;
    const p1 = p0 + 50_000;

    // delta de cupons entre p0 e p1
    const deltaCupons = Math.floor(p1 / 500_000) - Math.floor(p0 / 500_000);

    geral.pontos = p1;
    geral.cupons = (geral.cupons || 0) + deltaCupons;

    await this.geralRepository.save(geral);

    usuario.ultimaPontuacaoSemanal = hoje.toDate();
    await this.usuarioRepository.save(usuario);

    return 'Ponto de acesso semanal computado com sucesso';
  }

  /**
   * Retorna os dados "crus" da tabela Geral (sem formatar e sem recalcular cupons),
   * para o frontend formatar e exibir.
   */
  async buscarGeral(equipe?: string): Promise<Geral[]> {
    const where: any = {};
    if (equipe) where.equipe = equipe;

    const dados = await this.geralRepository.find({
      where,
      order: { pontos: 'DESC' },
    });

    return dados; // sem toLocaleString / sem floor de cupons
  }

  /**
   * Simulação: setar pontos em 500.000 preservando cupons manuais.
   * Também ajusta cupons apenas pela diferença de faixas.
   */
  async simularPontuacaoCompleta(nome: string): Promise<string> {
    const geral = await this.geralRepository.findOne({ where: { nome } });
    if (!geral) return 'Usuário não encontrado na tabela Geral.';

    const p0 = geral.pontos || 0;
    const p1 = 500_000;

    const deltaCupons = Math.floor(p1 / 500_000) - Math.floor(p0 / 500_000);

    geral.pontos = p1;
    geral.cupons = (geral.cupons || 0) + deltaCupons;

    await this.geralRepository.save(geral);

    return `Pontuação atualizada para 500.000. Cupons agora: ${geral.cupons}`;
  }

  /**
   * Atualiza pontos pelos clientes_positivados (cada um vale 50.000 pontos)
   * e ajusta cupons SOMENTE pela diferença de faixas.
   */
  async atualizarPositivados(): Promise<void> {
    await this.geralRepository.query(`
      UPDATE geral g
      JOIN (
        SELECT
          rcacode,
          pontos                                       AS p0,
          pontos + (clientes_positivados * 50000)      AS p1
        FROM geral
      ) x ON x.rcacode = g.rcacode
      SET g.pontos = x.p1,
          g.cupons = g.cupons + (FLOOR(x.p1 / 500000) - FLOOR(x.p0 / 500000))
    `);
  }
}
