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

  async registrarAcesso(cpf: string): Promise<string> {
    const usuario = await this.usuarioRepository.findOne({ where: { cpf } });

    if (!usuario) {
      return 'Usuário não encontrado';
    }

    const hoje = moment();
    const ultimaPontuacao = usuario.ultimaPontuacaoSemanal
      ? moment(usuario.ultimaPontuacaoSemanal)
      : null;

    const mesmaSemana = ultimaPontuacao &&
      hoje.isoWeek() === ultimaPontuacao.isoWeek() &&
      hoje.year() === ultimaPontuacao.year();

    if (mesmaSemana) {
      return 'Usuário já recebeu ponto nesta semana';
    }

    const geral = await this.geralRepository.findOne({ where: { nome: usuario.nome } });

    if (!geral) {
      return 'Registro na tabela Geral não encontrado';
    }

    geral.pontos = (geral.pontos || 0) + 50000; // Adiciona 50.000 pontos por acesso
    await this.geralRepository.save(geral);

    usuario.ultimaPontuacaoSemanal = hoje.toDate();
    await this.usuarioRepository.save(usuario);

    return 'Ponto de acesso semanal computado com sucesso';
  }

  // ✅ Novo método para buscar os dados da tabela Geral
  async buscarGeral(equipe?: string): Promise<any[]> {
    const where: any = {};
  
    if (equipe) {
      where.equipe = equipe;
    }
  
    const dados = await this.geralRepository.find({
      where,
      order: {
        pontos: 'DESC',
      },
    });
  
    // Formata os dados antes de retornar
    return dados.map((item) => ({
      ...item,
      pontos: (item.pontos ?? 0).toLocaleString('pt-BR'),
      cupons: (item.cupons ?? 0).toLocaleString('pt-BR'),
      faturamento: (item.faturamento ?? 0).toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
      }),
    }));
  }
}  
